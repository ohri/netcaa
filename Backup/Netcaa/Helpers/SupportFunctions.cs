using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SupportFunctions
/// </summary>
public class SupportFunctions
{
    public SupportFunctions()
    {
    }

    public static string CreateTradeContentHtml(string TeamAAssetts, string TeamBAssetts,
        string TeamA, string TeamB)
    {
        //return 
        //@"<table border=0 cellpadding=6 cellspacing=3><tr><td style=''>"
        //+ TeamA + "-&gt; " + TeamB
        //+ "<br /><hr>"
        //+ TeamAAssetts
        //+ @"</td><td>"
        //+ TeamB + "-&gt; " + TeamA
        //+ "<br /><hr>"
        //+ TeamBAssetts
        //+ @"</td></tr></table>";

        return
            @"<div style='float: left; width: 47%;'><u>"
        + TeamB + "-&gt; " + TeamA
            //+ "<br /><hr>"
        + "</u><br/>"
        + TeamAAssetts
        + @"</div><div style='float: right; width: 47%;'><u>"
        + TeamA + "-&gt; " + TeamB
            //+ "<br /><hr>"
        + "</u><br/>"
        + TeamBAssetts
        + @"</div>";
    }
}
