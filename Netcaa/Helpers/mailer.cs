using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using Logger;

/// <summary>
/// Summary description for mailer
/// </summary>
public class mailer
{
    public mailer()
    {
    }

    public static bool sendSynchronousLeagueMail(String subject, String body, bool isHTML, String user)
    {
        SmtpClient client = new SmtpClient();

        MailMessage m = new MailMessage(
            System.Configuration.ConfigurationManager.AppSettings["FromAddress"],
            System.Configuration.ConfigurationManager.AppSettings["LeagueAddress"],
            subject,
            body);
        m.IsBodyHtml = isHTML;

        try
        {
            client.Send(m);
            return true;
        }
        catch (Exception ex)
        {
            try
            {
                // retry
                client.Send(m);
                return true;
            }
            catch
            {
                try
                {
                    // one more time
                    client.Send(m);
                    return true;
                }
                catch
                {
                    Log.AddLogEntry(Logger.LogEntryTypes.SystemError,
                        user,
                        "Failed to send message from "
                        + System.Configuration.ConfigurationManager.AppSettings["FromAddress"]
                        + " to "
                        + System.Configuration.ConfigurationManager.AppSettings["LeagueAddress"]
                        + ", got following exception: " + ex.Message);
                    return false;
                }
            }
        }
    }

    public static bool sendSynchronousPrivateMail(string subject, string body, bool isHTML, string user, string toEmail, string fromEmail)
    {
        SmtpClient client = new SmtpClient();

        //if ( toEmail != "ramanohri@gmail.com" )
        //{
        //    toEmail = "ohri@sep.com";
        //}

        MailMessage m = new MailMessage(
            fromEmail,
            toEmail,
            subject,
            body);
        m.IsBodyHtml = isHTML;

        try
        {
            client.Send(m);
            return true;
        }
        catch (Exception ex)
        {
            // retry
            try
            {
                client.Send(m);
                return true;
            }
            catch
            {
                try
                {
                    // one last time
                    client.Send(m);
                    return true;
                }
                catch
                {
                    Log.AddLogEntry(Logger.LogEntryTypes.SystemError,
                        user,
                        "Failed to send message from "
                        + fromEmail
                        + " to "
                        + toEmail
                        + ", got following exception: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
