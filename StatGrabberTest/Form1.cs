using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace StatGrabberTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MonthCalendar mcCalendar;
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
				if (components != null) 
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
			this.mcCalendar = new System.Windows.Forms.MonthCalendar();
			this.ButtonGetStats = new System.Windows.Forms.Button();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.ButtonClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mcCalendar
			// 
			this.mcCalendar.Location = new System.Drawing.Point(56, 24);
			this.mcCalendar.Name = "mcCalendar";
			this.mcCalendar.TabIndex = 0;
			// 
			// ButtonGetStats
			// 
			this.ButtonGetStats.Location = new System.Drawing.Point(112, 208);
			this.ButtonGetStats.Name = "ButtonGetStats";
			this.ButtonGetStats.TabIndex = 1;
			this.ButtonGetStats.Text = "Go";
			this.ButtonGetStats.Click += new System.EventHandler(this.ButtonGetStats_Click);
			// 
			// tbOutput
			// 
			this.tbOutput.Location = new System.Drawing.Point(272, 24);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ReadOnly = true;
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(368, 200);
			this.tbOutput.TabIndex = 2;
			this.tbOutput.Text = "";
			// 
			// ButtonClear
			// 
			this.ButtonClear.Location = new System.Drawing.Point(416, 232);
			this.ButtonClear.Name = "ButtonClear";
			this.ButtonClear.TabIndex = 3;
			this.ButtonClear.Text = "Clear";
			this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 266);
			this.Controls.Add(this.ButtonClear);
			this.Controls.Add(this.tbOutput);
			this.Controls.Add(this.ButtonGetStats);
			this.Controls.Add(this.mcCalendar);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void ButtonClear_Click(object sender, System.EventArgs e)
		{
			this.tbOutput.Clear();
		}

		private void ButtonGetStats_Click(object sender, System.EventArgs e)
		{
			tbOutput.Text += "\r\n" + mcCalendar.SelectionStart.ToString() + "\r\n";

			// call the stat grabber
			StatGrabber.StatGrabber sg = new StatGrabber.StatGrabber();

			// dump out some results
			ArrayList urls = sg.GetGames( this.mcCalendar.SelectionStart );

			this.tbOutput.Text += "Ran GetGames, got back " + urls.Count + " games\r\n";

			ArrayList perfs = sg.GetPerformances( urls );
			tbOutput.Text += "Ran GetPerformances, got back " + perfs.Count + " perfs\r\n";

			SqlConnection con = new SqlConnection( @"data source=localhost\sqlexpress;initial catalog=netcaa;user id=netcaa_web;password=go_muddogs07!;packet size=4096" );
			ArrayList problems = sg.SavePerformances( con, perfs, mcCalendar.SelectionStart );
			tbOutput.Text += "Ran SavePerformances, got back " + problems.Count + " problems\r\n";
			foreach( StatGrabber.PlayerPerformance p in problems )
			{
				tbOutput.Text += p.FirstName + " " + p.LastName + " " + p.TeamName + "\r\n";
			}
		}
	}
}
