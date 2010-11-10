using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;
using Logger;

namespace netcaa.Pages
{
    public partial class TradePropose : System.Web.UI.Page
    {
        private int m_TradeId;
        private int m_TeamAId;
        private int m_TeamBId;

        protected enum TradeActions
        {
            Propose,
            Reject,
            Confirm,
            Announce
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            // validate the tradeid passed in
            if (Request.QueryString["TradeId"] == null)
            {
                // this is bad
                Response.Redirect("/Static/bad_params.html");
            }

            // make sure this is not the trade deadzone (between week 15
            // and end of season
            int Allowed = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spIsTradingAllowed");
            if (Allowed == 0)
            {
                // not allowed to see
                Response.Redirect("/Static/no_trading_allowed.html");
            }

            // validate that the current user is allowed see this
            m_TradeId = int.Parse(Request.QueryString["TradeId"]);
            Allowed = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spUserAllowedToSeeTrade",
                m_TradeId,
                Page.User.Identity.Name);
            if (Allowed == 0)
            {
                // not allowed to see
                Response.Redirect("/Static/not_your_trade.html");
            }

            DataSet TradeInfo = SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTradeInfo",
                m_TradeId);
            m_TeamAId = (int)TradeInfo.Tables[0].Rows[0]["TeamA"];
            m_TeamBId = (int)TradeInfo.Tables[0].Rows[0]["TeamB"];

            if (!IsPostBack)
            {
                lblTeamA.Text = TradeInfo.Tables[0].Rows[0]["TeamAName"].ToString();
                lblTeamB.Text = TradeInfo.Tables[0].Rows[0]["TeamBName"].ToString();

                // set button states appropriately
                if (TradeInfo.Tables[0].Rows[0]["State"].ToString().Equals("Completed"))
                {
                    btnConfirm.Visible = false;
                    btnPropose.Visible = false;
                    btnReject.Visible = false;
                    btnCancel.Visible = false;
                    cblAssetsA.Enabled = false;
                    cblAssetsB.Enabled = false;
                }
                else if (TradeInfo.Tables[0].Rows[0]["State"].ToString().Equals("New"))
                {
                    btnConfirm.Visible = false;
                    btnPropose.Visible = true;
                    btnReject.Visible = false;
                    btnCancel.Visible = true;
                }
                else if (TradeInfo.Tables[0].Rows[0]["State"].ToString().Contains("Confirmed"))
                {
                    btnConfirm.Visible = true;
                    btnPropose.Visible = true;
                    btnReject.Visible = true;
                    btnCancel.Visible = false;
                }
                else if (TradeInfo.Tables[0].Rows[0]["State"].ToString().Contains("Proposed"))
                {
                    btnConfirm.Visible = true;
                    btnPropose.Visible = true;
                    btnReject.Visible = true;
                    btnCancel.Visible = false;
                }

                // fetch trade details            
                DataSet TradeDetails = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTradeDetails",
                    m_TradeId);

                DataSet TradeTeams = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTradeTeams",
                    m_TradeId);
                m_TeamAId = (int)TradeTeams.Tables[0].Rows[0]["teamid"];
                m_TeamBId = (int)TradeTeams.Tables[0].Rows[1]["teamid"];

                // fetch team A assets and populate
                // fetch roster
                DataSet RosterA = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetRoster",
                    m_TeamAId);

                // fetch draft picks for next two seasons
                DataSet PicksA = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTeamFuturePicks",
                    m_TeamAId);

                // fetch team B assets and populate
                // fetch roster
                DataSet RosterB = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetRoster",
                    m_TeamBId);

                // fetch draft picks for next two seasons
                DataSet PicksB = SqlHelper.ExecuteDataset(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTeamFuturePicks",
                    m_TeamBId);

                // populate controls
                tbConditionals.Text = TradeInfo.Tables[0].Rows[0]["Conditional"].ToString();
                tblLastComment.Text = TradeInfo.Tables[0].Rows[0]["Comments"].ToString();

                foreach (DataRow row in RosterA.Tables[0].Rows)
                {
                    cblAssetsA.Items.Add(new ListItem(row["FullPlayer"].ToString(), "p" + row["PlayerId"].ToString()));
                }
                foreach (DataRow row in PicksA.Tables[0].Rows)
                {
                    cblAssetsA.Items.Add(new ListItem(row["FullPick"].ToString(), "d" + row["DraftPickId"].ToString()));
                }
                foreach (DataRow row in RosterB.Tables[0].Rows)
                {
                    cblAssetsB.Items.Add(new ListItem(row["FullPlayer"].ToString(), "p" + row["PlayerId"].ToString()));
                }
                foreach (DataRow row in PicksB.Tables[0].Rows)
                {
                    cblAssetsB.Items.Add(new ListItem(row["FullPick"].ToString(), "d" + row["DraftPickId"].ToString()));
                }

                foreach (DataRow row in TradeDetails.Tables[0].Rows)
                {
                    CheckBoxList list = null;
                    int id = -1;

                    int playerTeamId = -1;
                    int pickTeamId = -1;
                    if (int.TryParse(row["PlayerTeamId"].ToString(), out playerTeamId))
                    {
                        if (playerTeamId == m_TeamAId)
                        {
                            list = cblAssetsA;
                            id = m_TeamAId;
                        }
                        else
                        {
                            list = cblAssetsB;
                            id = m_TeamBId;
                        }
                    }
                    else if (int.TryParse(row["PickTeamId"].ToString(), out pickTeamId))
                    {
                        if (pickTeamId == m_TeamAId)
                        {
                            list = cblAssetsA;
                            id = m_TeamAId;
                        }
                        else
                        {
                            list = cblAssetsB;
                            id = m_TeamBId;
                        }
                    }
                    else
                    {
                        // epic fail
                        // TODO do something intelligent here
                    }

                    if (playerTeamId > 0)
                    {
                        ListItem item = list.Items.FindByValue("p" + row["PlayerId"].ToString());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                    else if (pickTeamId > 0)
                    {
                        ListItem item = list.Items.FindByValue("d" + row["DraftPickId"].ToString());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }
        protected void btnPropose_Click(object sender, EventArgs e)
        {
            // make sure this trade has not been completed
            DataSet TradeInfo = SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTradeInfo",
                m_TradeId);
            if (TradeInfo.Tables[0].Rows[0]["State"].ToString().Equals("Completed"))
            {
                Response.Redirect("/Static/trade_already_completed.html");
            }

            // clear out the trade items currently in for this trade
            int DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spClearTradeItems",
                m_TradeId);

            // create the new trade items
            AddTradeItems(m_TradeId, m_TeamBId, cblAssetsA.Items);
            AddTradeItems(m_TradeId, m_TeamAId, cblAssetsB.Items);

            // mark trade as proposed
            DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spMarkTradeProposed",
                m_TradeId);

            // update trade info
            DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spUpdateTradeInfo",
                m_TradeId,
                tbConditionals.Text,
                tbComments.Text,
                Page.User.Identity.Name);

            // email proposal
            int currentTeamId = DBUtilities.GetUsersTeamId(Page.User.Identity.Name);
            SendTradeEmail(m_TradeId, TradeActions.Propose, GetOtherTeamId(currentTeamId));
            Response.Redirect("/Pages/TradeList.aspx");
        }

        protected void AddTradeItems(int TradeId, int TeamId, ListItemCollection List)
        {
            foreach (ListItem i in List)
            {
                if (i.Selected)
                {
                    int Id = int.Parse(i.Value.Substring(1));
                    int PlayerId = -1;
                    int PickId = -1;
                    if (i.Value.ToCharArray()[0] == 'd')
                    {
                        PickId = Id;
                    }
                    else
                    {
                        PlayerId = Id;
                    }

                    int DontCare = (int)SqlHelper.ExecuteScalar(
                        System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                        "spAddTradeItem",
                        TradeId,
                        TeamId,
                        PickId,
                        PlayerId
                    );
                }
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            // make sure this trade has not been rejected or completed
            DataSet TradeInfo = SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTradeInfo",
                m_TradeId);
            if (TradeInfo.Tables[0].Rows[0]["State"].ToString().Equals("Rejected")
                || TradeInfo.Tables[0].Rows[0]["State"].ToString().Equals("Completed"))
            {
                Response.Redirect("/Static/trade_already_completedorrejected.html");
            }

            // before anyone goes confirming anything, make this trade is still
            // viable (i.e., the players and picks are still where they belong)
            int valid = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spTradeStillValid",
                m_TradeId);
            if (valid != 1)
            {
                Response.Redirect("/Static/trade_isnt_valid.html");
            }

            int currentTeamId = DBUtilities.GetUsersTeamId(Page.User.Identity.Name);

            // mark the trade record appropriately
            int DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spMarkTradeConfirmed",
                m_TradeId,
                currentTeamId);

            // update trade info
            DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spUpdateTradeInfo",
                m_TradeId,
                tbConditionals.Text,
                tbComments.Text,
                Page.User.Identity.Name);

            // if both sides have confirmed, execute and announce
            int IsConfirmed = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spHaveBothConfirmed",
                m_TradeId);

            if (IsConfirmed == 1)
            {
                DontCare = (int)SqlHelper.ExecuteScalar(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spExecuteTrade",
                    m_TradeId);

                SendTradeEmail(m_TradeId, TradeActions.Announce, -1);

                Log.AddLogEntry(LogEntryTypes.TradeExecuted,
                    Page.User.Identity.Name,
                    "Trade " + m_TradeId.ToString() + " executed");
            }
            else
            {
                // else, alert the other party of the confirmation
                SendTradeEmail(m_TradeId, TradeActions.Confirm, GetOtherTeamId(currentTeamId));
            }
            Response.Redirect("/Pages/TradeList.aspx");
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            // figure out which team is the other team
            int currentTeamId = DBUtilities.GetUsersTeamId(Page.User.Identity.Name);
            int otherteamid = GetOtherTeamId(currentTeamId);

            // update trade info
            int DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spUpdateTradeInfo",
                m_TradeId,
                tbConditionals.Text,
                tbComments.Text,
                Page.User.Identity.Name);

            // mark the trade rejected
            DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spMarkTradeRejected",
                m_TradeId,
                currentTeamId);

            // alert the other party of the rejection
            SendTradeEmail(m_TradeId, TradeActions.Reject, otherteamid);
            Response.Redirect("/Pages/TradeList.aspx");
        }

        protected int GetOtherTeamId(int currentteamid)
        {
            return (currentteamid == m_TeamAId) ? m_TeamBId : m_TeamAId;
        }

        protected void SendTradeEmail(int TradeId, TradeActions Operation, int Recipient)
        {
            // fetch trade contents
            string TeamAAssetts = SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTradeAssetts",
                m_TradeId,
                1).ToString();

            string TeamBAssetts = SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTradeAssetts",
                m_TradeId,
                2).ToString();

            // fetch trade information
            DataSet dc = SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetTradeInfo",
                m_TradeId);
            string TeamA = dc.Tables[0].Rows[0]["TeamAName"].ToString();
            string TeamB = dc.Tables[0].Rows[0]["TeamBName"].ToString();
            string Conditional = dc.Tables[0].Rows[0]["Conditional"].ToString();
            string Comments = dc.Tables[0].Rows[0]["Comments"].ToString();
            Comments = Comments.Replace(Environment.NewLine, @"<br />");

            // form email contents
            string subject = "";
            string body = "<font face=calibri>";

            // Subject
            string sendingTeam = (m_TeamAId == Recipient) ? TeamB : TeamA;
            switch (Operation)
            {
                case TradeActions.Announce:
                    subject = "Official League Trade Announcement";
                    break;
                case TradeActions.Confirm:
                    subject = "Trade confirmed by " + sendingTeam;
                    break;
                case TradeActions.Propose:
                    subject = "Trade proposed by " + sendingTeam;
                    break;
                case TradeActions.Reject:
                    subject = "Trade rejected by " + sendingTeam;
                    break;
            }

            // Content
            string Contents =
                "<table border=0 width=415px><tr><td>"
                + SupportFunctions.CreateTradeContentHtml(TeamAAssetts, TeamBAssetts, TeamA, TeamB)
                + "</td></tr></table>";

            if (Operation == TradeActions.Propose)
            {
                body += sendingTeam + " proposed a trade with you: <br /><br />";
                body += Contents;
                body += "<br /> Conditional Information: <br />";
                body += Conditional;
                body += "<br /><br /> Comments: <br />";
                body += Comments;
                body += "<br /><br /> Please respond via the league site.";
            }
            else if (Operation == TradeActions.Reject)
            {
                body += sendingTeam + " rejected the following trade with you: <br /><br />";
                body += Contents;
                body += "<br /> Conditional Information: <br />";
                body += Conditional;
                body += "<br /><br /> Comments: <br />";
                body += Comments;
            }
            else if (Operation == TradeActions.Announce)
            {
                body += TeamA + " and " + TeamB + " have completed a trade:<br /><br />";
                body += Contents;
                body += "<br /> Conditional Information: <br />";
                body += Conditional;
            }
            else if (Operation == TradeActions.Confirm)
            {
                body += sendingTeam + " confirmed a trade with you: <br /><br />";
                body += Contents;
                body += "<br /> Conditional Information: <br />";
                body += Conditional;
                body += "<br /><br /> Comments: <br />";
                body += Comments;
                body += "<br /><br /> Please respond via the league site.";
            }

            body += "</font>";
            // flush email
            if (Operation == TradeActions.Announce)
            {
                mailer.sendSynchronousLeagueMail(
                    subject,
                    body,
                    true,
                    Page.User.Identity.Name);

                mailer.sendSynchronousPrivateMail(
                    subject,
                    body,
                    true,
                    Page.User.Identity.Name,
                    System.Configuration.ConfigurationManager.AppSettings["OCAddress"],
                    System.Configuration.ConfigurationManager.AppSettings["FromAddress"]);
            }
            else
            {
                string toEmail = SqlHelper.ExecuteScalar(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTeamEmail",
                    Recipient).ToString();

                string fromEmail = SqlHelper.ExecuteScalar(
                    System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                    "spGetTeamEmail",
                    (Recipient == m_TeamAId) ? m_TeamBId : m_TeamAId).ToString();

                mailer.sendSynchronousPrivateMail(
                    subject,
                    body,
                    true,
                    Page.User.Identity.Name,
                    toEmail,
                    fromEmail);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int DontCare = (int)SqlHelper.ExecuteScalar(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spCancelTrade",
                m_TradeId);

            Response.Redirect("/Pages/TradeList.aspx");
        }
    }
}