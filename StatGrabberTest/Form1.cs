using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace StatGrabberTest
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MonthCalendar calStatDate;
        private System.Windows.Forms.Button ButtonGetStats;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button ButtonClear;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if( components != null )
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calStatDate = new System.Windows.Forms.MonthCalendar();
            this.ButtonGetStats = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // calStatDate
            // 
            this.calStatDate.Location = new System.Drawing.Point( 18, 12 );
            this.calStatDate.Name = "calStatDate";
            this.calStatDate.TabIndex = 0;
            // 
            // ButtonGetStats
            // 
            this.ButtonGetStats.Location = new System.Drawing.Point( 98, 231 );
            this.ButtonGetStats.Name = "ButtonGetStats";
            this.ButtonGetStats.Size = new System.Drawing.Size( 90, 27 );
            this.ButtonGetStats.TabIndex = 1;
            this.ButtonGetStats.Text = "Go";
            this.ButtonGetStats.Click += new System.EventHandler( this.ButtonGetStats_Click );
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point( 299, 12 );
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size( 619, 406 );
            this.tbOutput.TabIndex = 2;
            // 
            // ButtonClear
            // 
            this.ButtonClear.Location = new System.Drawing.Point( 197, 392 );
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size( 90, 26 );
            this.ButtonClear.TabIndex = 3;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.Click += new System.EventHandler( this.ButtonClear_Click );
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size( 6, 15 );
            this.ClientSize = new System.Drawing.Size( 930, 430 );
            this.Controls.Add( this.ButtonClear );
            this.Controls.Add( this.tbOutput );
            this.Controls.Add( this.ButtonGetStats );
            this.Controls.Add( this.calStatDate );
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout( false );
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run( new Form1() );
        }

        private void ButtonClear_Click( object sender, System.EventArgs e )
        {
            this.tbOutput.Clear();
        }

        private void ButtonGetStats_Click( object sender, System.EventArgs e )
        {
            tbOutput.Text += "\r\n" + calStatDate.SelectionStart.ToString() + "\r\n";

            StatGrabber.StatGrabber sg = new StatGrabber.StatGrabber();

            ArrayList urls = sg.GetGames( calStatDate.SelectionStart );

            tbOutput.Text += "Ran GetGames, got back " + urls.Count + " games\r\n";

            SqlDatabase db = new SqlDatabase( @"data source=localhost\sqlexpress;initial catalog=netcaa;user id=netcaa_web;password=go_muddogs07!;Persist Security Info=true" );
            ArrayList problems = new ArrayList();
            foreach( string url in urls )
            {
                try
                {
                    ArrayList perfs = sg.GetGamePerformances( url );
                    tbOutput.Text += "Got " + perfs.Count + " perfs from " + url + "\r\n";
                    problems.AddRange( sg.SavePerformances( db, perfs, calStatDate.SelectionStart ) );
                }
                catch( StatGrabber.StatGrabberException ex )
                {
                    tbOutput.Text += ex.Message;
                }
            }

            if( problems.Count > 0 )
            {
                foreach( StatGrabber.PlayerPerformance p in problems )
                {
                    tbOutput.Text += p.FirstName + " " + p.LastName + " " + p.TeamName + "\r\n";
                }
            }
            else
            {
                tbOutput.Text += "No problems identifying players\r\n";
            }

            tbOutput.Text += sg.UpdateAveragesAndScores( db, calStatDate.SelectionStart );
        }
    }
}
