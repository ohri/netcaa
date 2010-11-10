using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Logger;

/// <summary>
/// Summary description for AutoSub
/// </summary>
public class AutoSub
{
    public AutoSub()
    {
    }

    static public string ProcessAutosubs(string week)
    {
        int weekid = Int32.Parse(week);
        // get the lineups for the week
        DataSet ds = SqlHelper.ExecuteDataset(
            System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
            "spGetLineupInfoForWeek",
            weekid);

        string results = "";
        foreach (DataRow r in ds.Tables[0].Rows)
        {
            int teamid;
            int gameid;
            int.TryParse(r["teamid"].ToString(), out teamid);
            int.TryParse(r["gameid"].ToString(), out gameid);
            results += ProcessLineup(teamid, gameid);
        }

        Log.AddLogEntry(LogEntryTypes.AutosubExecuted, "System", results, System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
        return results;
    }

    static protected string ProcessLineup(int teamid, int gameid)
    {
        string retval = "";

        // check the starters
        for (int i = 0; i < 5; i++)
        {
            // need to do this inside the loop in case we actually
            // did some swapping on the last iteration
            // optimization: only reload when neccesary

            // fetch the lineup records
            DataSet ds = SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetLineupInfoForAutosub",
                gameid,
                teamid);

            if ((int)ds.Tables[0].Rows[i]["GamesPlayed"] == 0)
            {
                // check the backup
                if ((int)ds.Tables[0].Rows[i + 5]["GamesPlayed"] != 0)
                {
                    retval += SwapPlayers(
                        (int)ds.Tables[0].Rows[i]["LineupId"],
                        (int)ds.Tables[0].Rows[i + 5]["LineupId"])
                            + ' ' + ds.Tables[0].Rows[i]["Player"].ToString()
                            + ' ' + ds.Tables[0].Rows[i + 5]["Player"].ToString();
                }
                else
                {
                    // backup hasn't played, check garbage 1
                    if ((int)ds.Tables[0].Rows[10]["GamesPlayed"] != 0
                        && IsPlayerEligible(ds.Tables[0].Rows[10]["PlayerPosition"].ToString(), i + 1))
                    {
                        retval += SwapPlayers(
                            (int)ds.Tables[0].Rows[i]["LineupId"],
                            (int)ds.Tables[0].Rows[10]["LineupId"])
                            + ' ' + ds.Tables[0].Rows[i]["Player"].ToString()
                            + ' ' + ds.Tables[0].Rows[10]["Player"].ToString();
                    }
                    // garbage 1 hasnt played or isnt elibible so check garbge 2
                    else if ((int)ds.Tables[0].Rows[11]["GamesPlayed"] != 0
                        && IsPlayerEligible(ds.Tables[0].Rows[11]["PlayerPosition"].ToString(), i + 1))
                    {
                        retval += SwapPlayers(
                            (int)ds.Tables[0].Rows[i]["LineupId"],
                            (int)ds.Tables[0].Rows[11]["LineupId"])
                            + ' ' + ds.Tables[0].Rows[i]["Player"].ToString()
                            + ' ' + ds.Tables[0].Rows[11]["Player"].ToString();

                    }
                    else
                    {
                        // no sub is available!
                        retval += ", no swap available for lineupid " + ds.Tables[0].Rows[i]["LineupId"].ToString()
                            + ' ' + ds.Tables[0].Rows[i]["Player"].ToString();
                    }
                }
            }
        }

        // TODO: handle no player there in a garbage slot

        // check the backups
        for (int i = 5; i < 10; i++)
        {
            // need to do this inside the loop in case we actually
            // did some swapping on the last iteration
            // optimization: only reload when neccesary

            // fetch the lineup records
            DataSet ds = SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
                "spGetLineupInfoForAutosub",
                gameid,
                teamid);

            if ((int)ds.Tables[0].Rows[i]["GamesPlayed"] == 0)
            {
                // check garbage 1
                if ((int)ds.Tables[0].Rows[10]["GamesPlayed"] != 0
                    && IsPlayerEligible(ds.Tables[0].Rows[10]["PlayerPosition"].ToString(), i + 1))
                {
                    retval += SwapPlayers(
                        (int)ds.Tables[0].Rows[i]["LineupId"],
                        (int)ds.Tables[0].Rows[10]["LineupId"])
                            + ' ' + ds.Tables[0].Rows[i]["Player"].ToString()
                            + ' ' + ds.Tables[0].Rows[10]["Player"].ToString();
                }
                // garbage 1 hasnt played or isnt elibible so check garbge 2
                else if ((int)ds.Tables[0].Rows[11]["GamesPlayed"] != 0
                    && IsPlayerEligible(ds.Tables[0].Rows[11]["PlayerPosition"].ToString(), i + 1))
                {
                    retval += SwapPlayers(
                        (int)ds.Tables[0].Rows[i]["LineupId"],
                        (int)ds.Tables[0].Rows[11]["LineupId"])
                            + ' ' + ds.Tables[0].Rows[i]["Player"].ToString()
                            + ' ' + ds.Tables[0].Rows[11]["Player"].ToString();
                }
                else
                {
                    // no sub is available!
                    retval += ", no swap available for lineupid " + ds.Tables[0].Rows[i]["LineupId"].ToString()
                        + ' ' + ds.Tables[0].Rows[i]["Player"].ToString();
                }
            }
        }

        return retval;
    }

    static protected string SwapPlayers(int LineupAId, int LineupBId)
    {
        int dc = (int)SqlHelper.ExecuteScalar(
            System.Configuration.ConfigurationManager.AppSettings["ConnectionString"],
            "spAutosub",
            LineupAId,
            LineupBId);
        return ", swapped " + LineupAId + " for " + LineupBId;
        //return ", would have swapped " + LineupAId + " for " + LineupBId;
    }

    static protected bool IsPlayerEligible(string PlayerPosition, int LineupPosition)
    {
        PlayerPosition = PlayerPosition.Trim();

        switch (LineupPosition)
        {
            case 1:
            case 6:
                if (PlayerPosition.Equals("G"))
                {
                    return true;
                }
                break;

            case 2:
            case 7:
                if (PlayerPosition.Equals("G")
                    || PlayerPosition.Equals("GF")
                    || PlayerPosition.Equals("FG")
                    )
                {
                    return true;
                }
                break;

            case 3:
            case 8:
                if (PlayerPosition.Equals("F")
                    || PlayerPosition.Equals("GF")
                    || PlayerPosition.Equals("FG")
                    )
                {
                    return true;
                }
                break;

            case 4:
            case 9:
                if (PlayerPosition.Equals("F")
                    || PlayerPosition.Equals("FC")
                    || PlayerPosition.Equals("CF")
                    )
                {
                    return true;
                }
                break;

            case 5:
            case 10:
                if (PlayerPosition.Equals("C")
                    || PlayerPosition.Equals("CF")
                    || PlayerPosition.Equals("FC")
                    )
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
