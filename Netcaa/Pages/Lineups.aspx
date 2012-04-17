<%@ Page Language="c#" Inherits="netcaa.Pages.Lineups" CodeBehind="Lineups.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="footer" Src="/Controls/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="navbar" Src="/Controls/navbar.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Lineup Submission</title>
    <link href="/Styles/netcaa.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/lineup_validation.js"></script>
    <script language="javascript" type="text/javascript">

        var pos_labels = [
            "#lblPG",
            "#lblSG",
            "#lblSF",
            "#lblPF",
            "#lblC",
            "#lblBackupPG",
            "#lblBackupSG",
            "#lblBackupSF",
            "#lblBackupPF",
            "#lblBackupC",
            "#lblGarbage1",
            "#lblGarbage2"
            ];

        function enable_submit()
        {
            if ( $( "#SPG" ).html() == "" && $( '#hiddenSPG' ).val() > 0
                && $( "#SSG" ).html() == "" && $( '#hiddenSSG' ).val() > 0
                && $( "#SSF" ).html() == "" && $( '#hiddenSSF' ).val() > 0
                && $( "#SPF" ).html() == "" && $( '#hiddenSPF' ).val() > 0
                && $( "#SC" ).html() == "" && $( '#hiddenSC' ).val() > 0
                && $( "#BPG" ).html() == "" && $( '#hiddenBPG' ).val() > 0
                && $( "#BSG" ).html() == "" && $( '#hiddenBSG' ).val() > 0
                && $( "#BSF" ).html() == "" && $( '#hiddenBSF' ).val() > 0
                && $( "#BPF" ).html() == "" && $( '#hiddenBPF' ).val() > 0
                && $( "#BC" ).html() == "" && $( '#hiddenBC' ).val() > 0
                && $( "#G1" ).html() == "" && $( '#hiddenG1' ).val() > 0
                && $( "#G2" ).html() == "" && $( '#hiddenG2' ).val() > 0
            )
            {
                $( "#ButtonSubmitLineup" ).removeAttr( "disabled" );
            }
            else
            {
                $( "#ButtonSubmitLineup" ).attr( "disabled", true );
            }
        }

        $( document ).ready( function ()
        {
            enable_submit();
        } );

    </script>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" cellpadding="8">
        <tr>
            <td>
                <uc1:navbar ID="Navbar1" runat="server"></uc1:navbar>
            </td>
            <td>
                <br />
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <asp:Label ID="lblPageTitle" runat="server" CssClass="pagetitle">Lineup Submission</asp:Label>
                <hr align="left" width="100%" color="red" size="1" />
                <p></p>
                <div>
                    <asp:Panel ID="PlayerSelection" runat="server" Visible="False" BorderStyle="None"
                        Height="396px">
                        <p>
                            <asp:Label ID="Label1" runat="server" Height="40px" Width="672px">Instructions: Select a player from the Roster listbox on the left and click on the desired place on the depth chart on the right. Note: the tool will not stop you from putting in a bogus lineup!</asp:Label></p>
                        <p>
                            <asp:Label ID="lblGameHeader" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label></p>
                        <div>
                            <table id="Table2" cellpadding="10" border="0">
                                <tr>
                                    <td>
                                        <p><strong><u>Roster</u></strong></p>
                                        <p><asp:ListBox ID="lbRoster" runat="server" SelectionMode="Single" Rows="12"></asp:ListBox></p>
                                    </td>
                                    <td>
                                        <p><strong><u>Starters</u></strong></p>
                                        <strong>PG:</strong>
                                        <asp:Label ID="lblPG" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#SPG").html( lineup_validate( x, 1 ) );
                                                     if( $("#SPG").html().length == 0 )
                                                     {
                                                         $("#hiddenSPG").val( $("#lbRoster").val() );
                                                         $("#lblPG").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="SPG" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>SG</strong>:
                                        <asp:Label ID="lblSG" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#SSG").html( lineup_validate( x, 2 ) );
                                                     if( $("#SSG").html().length == 0 )
                                                     {
                                                         $("#hiddenSSG").val( $("#lbRoster").val() );
                                                         $("#lblSG").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="SSG" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>SF</strong>:
                                        <asp:Label ID="lblSF" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#SSF").html( lineup_validate( x, 3 ) );
                                                     if( $("#SSF").html().length == 0 )
                                                     {
                                                         $("#hiddenSSF").val( $("#lbRoster").val() );
                                                         $("#lblSF").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="SSF" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>PF</strong>:
                                        <asp:Label ID="lblPF" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#SPF").html( lineup_validate( x, 4 ) );
                                                     if( $("#SPF").html().length == 0 )
                                                     {
                                                         $("#hiddenSPF").val( $("#lbRoster").val() );
                                                         $("#lblPF").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="SPF" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>C&nbsp; </strong>:
                                        <asp:Label ID="lblC" runat="server" CssClass="handcursor" BorderStyle="Solid" Width="160px"
                                            Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#SC").html( lineup_validate( x, 5 ) );
                                                     if( $("#SC").html().length == 0 )
                                                     {
                                                         $("#hiddenSC").val( $("#lbRoster").val() );
                                                         $("#lblC").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label>
                                        <p id="SC" class="lineup_validation_error"></p>
                                    </td>
                                    <td>
                                        <p><strong><u>Backups</u></strong></p>
                                        <strong>PG</strong>:
                                        <asp:Label ID="lblBackupPG" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#BPG").html( lineup_validate( x, 6 ) );
                                                     if( $("#BPG").html().length == 0 )
                                                     {
                                                         $("#hiddenBPG").val( $("#lbRoster").val() );
                                                         $("#lblBackupPG").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="BPG" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>SG</strong>:
                                        <asp:Label ID="lblBackupSG" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#BSG").html( lineup_validate( x, 7 ) );
                                                     if( $("#BSG").html().length == 0 )
                                                     {
                                                         $("#hiddenBSG").val( $("#lbRoster").val() );
                                                         $("#lblBackupSG").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="BSG" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>SF</strong>:
                                        <asp:Label ID="lblBackupSF" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#BSF").html( lineup_validate( x, 8 ) );
                                                     if( $("#BSF").html().length == 0 )
                                                     {
                                                         $("#hiddenBSF").val( $("#lbRoster").val() );
                                                         $("#lblBackupSF").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="BSF" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>PF</strong>:
                                        <asp:Label ID="lblBackupPF" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#BPF").html( lineup_validate( x, 9 ) );
                                                     if( $("#BPF").html().length == 0 )
                                                     {
                                                         $("#hiddenBPF").val( $("#lbRoster").val() );
                                                         $("#lblBackupPF").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label><br />
                                        <p id="BPF" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>C&nbsp;</strong>&nbsp;:
                                        <asp:Label ID="lblBackupC" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                            onClick='var x = $("#lbRoster :selected").text();
                                                     $("#BC").html( lineup_validate( x, 10 ) );
                                                     if( $("#BC").html().length == 0 )
                                                     {
                                                         $("#hiddenBC").val( $("#lbRoster").val() );
                                                         $("#lblBackupC").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                            Pick 'n Click</asp:Label>
                                        <p id="BC" class="lineup_validation_error"></p>
                                    </td>
                                    <td>
                                        <p>
                                            <strong><u>Garbage</u></strong></p>
                                        <strong>1</strong>:
                                        <asp:Label ID="lblGarbage1" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                                onClick='var x = $("#lbRoster :selected").text();
                                                     $("#G1").html( lineup_validate( x, 11 ) );
                                                     if( $("#G1").html().length == 0 )
                                                     {
                                                         $("#hiddenG1").val( $("#lbRoster").val() );
                                                         $("#lblGarbage1").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                                Pick 'n Click</asp:Label><br />
                                            <p id="G1" class="lineup_validation_error"></p>
                                        <br />
                                        <strong>2</strong>:
                                        <asp:Label ID="lblGarbage2" runat="server" CssClass="handcursor" BorderStyle="Solid"
                                            Width="160px" Font-Size="X-Small" BorderColor="Silver" BackColor="#E0E0E0" BorderWidth="1px"
                                                onClick='var x = $("#lbRoster :selected").text();
                                                     $("#G2").html( lineup_validate( x, 12 ) );
                                                     if( $("#G2").html().length == 0 )
                                                     {
                                                         $("#hiddenG2").val( $("#lbRoster").val() );
                                                         $("#lblGarbage2").html( x );
                                                     }
                                                     enable_submit();
                                                     '>
                                                Pick 'n Click</asp:Label>
                                            <p id="G2" class="lineup_validation_error"></p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <center><font size="3"><strong><u>Comments</u></strong></font></center>
                        <center><font size="2">&nbsp;(will show up in email):</font></center>
                        <br />
                        <center><asp:TextBox ID="tbComment" Rows="4" runat="server" TextMode="MultiLine"  Width="360px" /></center>
                        <br />
                        <center>
                            <asp:Button ID="ButtonSubmitLineup" runat="server" Text="Submit" OnClick="ButtonSubmitLineup_Click"></asp:Button>
                            <asp:Literal ID="litHiddenPlaceholder" runat="server"></asp:Literal>
                        </center>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="TeamWeekSelection" runat="server" BorderStyle="None">
                        <table cellspacing="20" border="0">
                            <tr>
                                <th>
                                    Select team:
                                </th>
                                <th>
                                    Select week:
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbTeams" runat="server" Rows="16"></asp:ListBox>
                                </td>
                                <td>
                                    <asp:ListBox ID="lbWeeks" runat="server" Rows="19"></asp:ListBox>
                                </td>
                            </tr>
                        </table>
                        <center>
                            <asp:Button ID="ButtonSubmitTeamWeek" runat="server" Text="Submit" OnClick="ButtonSubmitTeamWeek_Click">
                            </asp:Button></center>
                    </asp:Panel>
                </div>
                <p></p>
                <p></p>
                <p></p>
            </td>
        </tr>
    </table>
    <uc1:footer ID="Footer1" runat="server"></uc1:footer>
    </form>
</body>
</html>
