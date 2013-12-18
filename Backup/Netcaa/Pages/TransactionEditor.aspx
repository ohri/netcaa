<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="netcaa.Pages.TransactionEditor"
    Title="Transaction Editor" CodeBehind="TransactionEditor.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="Server">
    Transaction Editor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <script language="javascript" type="text/javascript" src="/Scripts/SelectSwapMove.js"></script>
    <table bordercolor="navy" cellspacing="0" cellpadding="20" border="1">
        <tr>
            <td style="height: 34px">
                <p>
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" Font-Underline="True">Signings</asp:Label></p>
                <table cellpadding="5" border="0">
                    <tr>
                        <td style="width: 203px; height: 163px;">
                            <asp:ListBox ID="lbComing" runat="server" Width="200px" Height="149px" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td style="height: 163px">
                            <asp:Button ID="btnAddSignings" runat="server" Width="72px" Text="Add" OnClick="btnAddSignings_Click">
                            </asp:Button><br />
                            <br />
                            <asp:Button ID="btnRemoveSigning" runat="server" Width="72px" Text="Remove" UseSubmitBehavior="False"
                                OnClick="btnRemoveSigning_Click"></asp:Button><br />
                            <br />
                            <asp:Button ID="btnUpSigning" runat="server" Width="72px" Text="Up" UseSubmitBehavior="False"
                                OnClick="btnUpSigning_Click"></asp:Button><br />
                            <br />
                            <asp:Button ID="btnDownSigning" runat="server" Width="72px" Text="Down" UseSubmitBehavior="False"
                                OnClick="btnDownSigning_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="height: 34px">
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="True" Font-Underline="True">Cuts</asp:Label><p>
                    <asp:Label ID="Label7" runat="server">Cut at least</asp:Label>
                    <asp:TextBox ID="tbCutAtLeast" runat="server" Width="32px"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server">players from:</asp:Label></p>
                <table cellpadding="5" border="0">
                    <tr>
                        <td style="width: 201px">
                            <asp:ListBox ID="lbGoing" runat="server" Width="200px" Height="148px"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAddCuts" runat="server" Width="72px" Text="Add" OnClick="btnAddCuts_Click">
                            </asp:Button><br />
                            <br />
                            <asp:Button ID="btnRemoveCut" runat="server" Width="72px" Text="Remove" UseSubmitBehavior="False"
                                OnClick="btnRemoveCut_Click"></asp:Button><br />
                            <br />
                            <asp:Button ID="btnUpCut" runat="server" Width="72px" Text="Up" UseSubmitBehavior="False"
                                OnClick="btnUpCut_Click"></asp:Button><br />
                            <br />
                            <asp:Button ID="btnDownCut" runat="server" Width="72px" Text="Down" UseSubmitBehavior="False"
                                OnClick="btnDownCut_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 116px">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" Font-Underline="True">Activations</asp:Label><br />
                <br />
                <asp:CheckBoxList ID="cblActivations" runat="server">
                </asp:CheckBoxList>
            </td>
            <td style="height: 116px">
                <p>
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="True" Font-Underline="True">Place on IR</asp:Label></p>
                <table cellpadding="5" border="0">
                    <tr>
                        <td style="width: 193px">
                            <asp:ListBox ID="lbDeactivate" runat="server" Width="192px"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAddToIR" runat="server" Width="72px" Text="Add" OnClick="btnAddToIR_Click">
                            </asp:Button>
                            <p>
                                <asp:Button ID="btnRemoveIR" runat="server" Width="72px" Text="Remove" UseSubmitBehavior="False"
                                    OnClick="btnRemoveIR_Click"></asp:Button>
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit Transaction" OnClick="btnSubmit_Click">
                </asp:Button>&nbsp;
                <asp:Button ID="btnDeleteTransaction" runat="server" Text="Delete Transaction" OnClick="btnDeleteTransaction_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
