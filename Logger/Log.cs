using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TextRepresentation;

namespace Logger
{
	/// <summary>
	/// Summary description for Log.
	/// </summary>

	public enum LogEntryTypes
	{
		[TextRep("Login")]
		Login,
		[TextRep("Failed Login")]
		FailedLogin,
		[TextRep("Changed Password")]
		ChangedPassword,
		[TextRep("Lineup Submission")]
		LineupSubmission,
		[TextRep("Transaction Submission")]
		TransactionSubmission,
		[TextRep("Stats Processed")]
		StatsProcessed,
		[TextRep("Week Finalized")]
		WeekFinalized,
		[TextRep("System Error")]
		SystemError,
        [TextRep( "Pick Submitted" )]
        PickSubmitted,
        [TextRep( "Trade Executed" )]
        TradeExecuted,
        [TextRep( "Trade Reversed" )]
        TradeReversed,
        [TextRep( "Transactions Processed" )]
        TransactionsProcessed,
        [TextRep( "Autosub Executed" )]
        AutosubExecuted
};
	
	public class Log
	{
		public Log()
		{
		}

        public static void AddLogEntry( LogEntryTypes type, string username, string entrytext )
        {
            SqlHelper.ExecuteDataset(
                System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"],
                "spAddLogEntry",
                TextualRepresentation.GetDescription( type ),
                entrytext,
                username );
        }

        public static void AddLogEntry( LogEntryTypes type, string username, string entrytext, string con )
        {
            SqlHelper.ExecuteDataset(
                con,
                "spAddLogEntry",
                TextualRepresentation.GetDescription( type ),
                entrytext,
                username );
        }
	}
}
