namespace BigDSignClientDesktop.GUI
{
	partial class EventGUI
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			listBoxEvent = new ListBox();
			groupBox1 = new GroupBox();
			btnRefresh = new Button();
			labelProcessTextEvent = new Label();
			eventListBox = new GroupBox();
			textStatusEvent = new ComboBox();
			textDateEvent = new DateTimePicker();
			labelProcessTextEventStatus = new Label();
			btnClearEvent = new Button();
			textDescriptionEvent = new RichTextBox();
			label5 = new Label();
			label1 = new Label();
			textPriceEvent = new TextBox();
			btnUpdateEvent = new Button();
			btnDeleteEvent = new Button();
			btnCreateEvent = new Button();
			label4 = new Label();
			label3 = new Label();
			label = new Label();
			label2 = new Label();
			textNameEvent = new TextBox();
			textIdEvent = new TextBox();
			eventCalendar = new MonthCalendar();
			label6 = new Label();
			groupBox1.SuspendLayout();
			eventListBox.SuspendLayout();
			SuspendLayout();
			// 
			// listBoxEvent
			// 
			listBoxEvent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			listBoxEvent.FormattingEnabled = true;
			listBoxEvent.ItemHeight = 15;
			listBoxEvent.Location = new Point(23, 43);
			listBoxEvent.Name = "listBoxEvent";
			listBoxEvent.Size = new Size(305, 304);
			listBoxEvent.TabIndex = 0;
			listBoxEvent.SelectedIndexChanged += ListBoxEvent_SelectedIndexChanged;
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupBox1.Controls.Add(btnRefresh);
			groupBox1.Controls.Add(labelProcessTextEvent);
			groupBox1.Controls.Add(listBoxEvent);
			groupBox1.Location = new Point(248, 10);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(353, 429);
			groupBox1.TabIndex = 2;
			groupBox1.TabStop = false;
			groupBox1.Text = "Event List";
			// 
			// btnRefresh
			// 
			btnRefresh.Location = new Point(253, 396);
			btnRefresh.Name = "btnRefresh";
			btnRefresh.Size = new Size(75, 23);
			btnRefresh.TabIndex = 7;
			btnRefresh.Text = "Refresh List";
			btnRefresh.UseVisualStyleBackColor = true;
			btnRefresh.Click += BtnRefresh_Click;
			// 
			// labelProcessTextEvent
			// 
			labelProcessTextEvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			labelProcessTextEvent.AutoSize = true;
			labelProcessTextEvent.Location = new Point(23, 350);
			labelProcessTextEvent.Margin = new Padding(2, 0, 2, 0);
			labelProcessTextEvent.Name = "labelProcessTextEvent";
			labelProcessTextEvent.Size = new Size(16, 15);
			labelProcessTextEvent.TabIndex = 6;
			labelProcessTextEvent.Text = "...";
			// 
			// eventListBox
			// 
			eventListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			eventListBox.Controls.Add(textStatusEvent);
			eventListBox.Controls.Add(textDateEvent);
			eventListBox.Controls.Add(labelProcessTextEventStatus);
			eventListBox.Controls.Add(btnClearEvent);
			eventListBox.Controls.Add(textDescriptionEvent);
			eventListBox.Controls.Add(label5);
			eventListBox.Controls.Add(label1);
			eventListBox.Controls.Add(textPriceEvent);
			eventListBox.Controls.Add(btnUpdateEvent);
			eventListBox.Controls.Add(btnDeleteEvent);
			eventListBox.Controls.Add(btnCreateEvent);
			eventListBox.Controls.Add(label4);
			eventListBox.Controls.Add(label3);
			eventListBox.Controls.Add(label);
			eventListBox.Controls.Add(label2);
			eventListBox.Controls.Add(textNameEvent);
			eventListBox.Controls.Add(textIdEvent);
			eventListBox.ImeMode = ImeMode.NoControl;
			eventListBox.Location = new Point(606, 10);
			eventListBox.Margin = new Padding(2, 1, 2, 1);
			eventListBox.Name = "eventListBox";
			eventListBox.Padding = new Padding(2, 1, 2, 1);
			eventListBox.Size = new Size(238, 428);
			eventListBox.TabIndex = 3;
			eventListBox.TabStop = false;
			eventListBox.Text = "Event information";
			// 
			// textStatusEvent
			// 
			textStatusEvent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			textStatusEvent.FormattingEnabled = true;
			textStatusEvent.Items.AddRange(new object[] { "Sold out", "Available" });
			textStatusEvent.Location = new Point(20, 138);
			textStatusEvent.Name = "textStatusEvent";
			textStatusEvent.Size = new Size(197, 23);
			textStatusEvent.TabIndex = 8;
			// 
			// textDateEvent
			// 
			textDateEvent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			textDateEvent.Location = new Point(20, 186);
			textDateEvent.Name = "textDateEvent";
			textDateEvent.Size = new Size(197, 23);
			textDateEvent.TabIndex = 9;
			textDateEvent.Format = DateTimePickerFormat.Custom;
			textDateEvent.CustomFormat = "ddd, d. MMM yyyy";
			// 
			// labelProcessTextEventStatus
			// 
			labelProcessTextEventStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			labelProcessTextEventStatus.AutoSize = true;
			labelProcessTextEventStatus.Location = new Point(20, 350);
			labelProcessTextEventStatus.Margin = new Padding(2, 0, 2, 0);
			labelProcessTextEventStatus.Name = "labelProcessTextEventStatus";
			labelProcessTextEventStatus.Size = new Size(16, 15);
			labelProcessTextEventStatus.TabIndex = 8;
			labelProcessTextEventStatus.Text = "...";
			// 
			// btnClearEvent
			// 
			btnClearEvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnClearEvent.Location = new Point(120, 395);
			btnClearEvent.Margin = new Padding(2, 1, 2, 1);
			btnClearEvent.Name = "btnClearEvent";
			btnClearEvent.Size = new Size(81, 24);
			btnClearEvent.TabIndex = 20;
			btnClearEvent.Text = "Clear";
			btnClearEvent.UseVisualStyleBackColor = true;
			btnClearEvent.Click += BtnClearEvent_Click;
			// 
			// textDescriptionEvent
			// 
			textDescriptionEvent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			textDescriptionEvent.Location = new Point(20, 279);
			textDescriptionEvent.Name = "textDescriptionEvent";
			textDescriptionEvent.Size = new Size(197, 69);
			textDescriptionEvent.TabIndex = 19;
			textDescriptionEvent.Text = "";
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new Point(20, 261);
			label5.Margin = new Padding(2, 0, 2, 0);
			label5.Name = "label5";
			label5.Size = new Size(67, 15);
			label5.TabIndex = 17;
			label5.Text = "Description";
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new Point(20, 214);
			label1.Margin = new Padding(2, 0, 2, 0);
			label1.Name = "label1";
			label1.Size = new Size(33, 15);
			label1.TabIndex = 15;
			label1.Text = "Price";
			// 
			// textPriceEvent
			// 
			textPriceEvent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			textPriceEvent.Location = new Point(19, 232);
			textPriceEvent.Margin = new Padding(2, 1, 2, 1);
			textPriceEvent.Name = "textPriceEvent";
			textPriceEvent.Size = new Size(198, 23);
			textPriceEvent.TabIndex = 14;
			// 
			// btnUpdateEvent
			// 
			btnUpdateEvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnUpdateEvent.Location = new Point(120, 365);
			btnUpdateEvent.Margin = new Padding(2, 1, 2, 1);
			btnUpdateEvent.Name = "btnUpdateEvent";
			btnUpdateEvent.Size = new Size(81, 24);
			btnUpdateEvent.TabIndex = 13;
			btnUpdateEvent.Text = "Update";
			btnUpdateEvent.UseVisualStyleBackColor = true;
			btnUpdateEvent.Click += BtnUpdateEvent_Click;
			// 
			// btnDeleteEvent
			// 
			btnDeleteEvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnDeleteEvent.Location = new Point(35, 395);
			btnDeleteEvent.Margin = new Padding(2, 1, 2, 1);
			btnDeleteEvent.Name = "btnDeleteEvent";
			btnDeleteEvent.Size = new Size(81, 24);
			btnDeleteEvent.TabIndex = 12;
			btnDeleteEvent.Text = "Delete";
			btnDeleteEvent.UseVisualStyleBackColor = true;
			btnDeleteEvent.Click += BtnDeleteEvent_Click;
			// 
			// btnCreateEvent
			// 
			btnCreateEvent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCreateEvent.Location = new Point(36, 365);
			btnCreateEvent.Margin = new Padding(2);
			btnCreateEvent.Name = "btnCreateEvent";
			btnCreateEvent.Size = new Size(80, 24);
			btnCreateEvent.TabIndex = 21;
			btnCreateEvent.Text = "Create";
			btnCreateEvent.Click += BtnCreateEvent_Click;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new Point(20, 168);
			label4.Margin = new Padding(2, 0, 2, 0);
			label4.Name = "label4";
			label4.Size = new Size(31, 15);
			label4.TabIndex = 8;
			label4.Text = "Date";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(20, 120);
			label3.Margin = new Padding(2, 0, 2, 0);
			label3.Name = "label3";
			label3.Size = new Size(39, 15);
			label3.TabIndex = 7;
			label3.Text = "Status";
			// 
			// label
			// 
			label.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label.AutoSize = true;
			label.Location = new Point(20, 27);
			label.Margin = new Padding(2, 0, 2, 0);
			label.Name = "label";
			label.Size = new Size(17, 15);
			label.TabIndex = 4;
			label.Text = "Id";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(20, 75);
			label2.Margin = new Padding(2, 0, 2, 0);
			label2.Name = "label2";
			label2.Size = new Size(39, 15);
			label2.TabIndex = 3;
			label2.Text = "Name";
			// 
			// textNameEvent
			// 
			textNameEvent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			textNameEvent.Location = new Point(20, 93);
			textNameEvent.Margin = new Padding(2, 1, 2, 1);
			textNameEvent.Name = "textNameEvent";
			textNameEvent.Size = new Size(197, 23);
			textNameEvent.TabIndex = 1;
			// 
			// textIdEvent
			// 
			textIdEvent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			textIdEvent.Enabled = false;
			textIdEvent.Location = new Point(19, 43);
			textIdEvent.Margin = new Padding(2, 1, 2, 1);
			textIdEvent.Name = "textIdEvent";
			textIdEvent.Size = new Size(198, 23);
			textIdEvent.TabIndex = 0;
			// 
			// eventCalendar
			// 
			eventCalendar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			eventCalendar.Location = new Point(11, 53);
			eventCalendar.Name = "eventCalendar";
			eventCalendar.TabIndex = 0;
			eventCalendar.DateChanged += EventCalendar_DateSelected;
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new Point(11, 29);
			label6.Margin = new Padding(2, 0, 2, 0);
			label6.Name = "label6";
			label6.Size = new Size(86, 15);
			label6.TabIndex = 16;
			label6.Text = "Event Calendar";
			// 
			// EventGUI
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(855, 449);
			Controls.Add(label6);
			Controls.Add(eventCalendar);
			Controls.Add(eventListBox);
			Controls.Add(groupBox1);
			Name = "EventGUI";
			Text = "Event management";
			FormClosing += EventGUI_FormClosing;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			eventListBox.ResumeLayout(false);
			eventListBox.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ListBox listBoxEvent;
		private GroupBox groupBox1;
		private GroupBox eventListBox;
		private Label label4;
		private Label label3;
		private Label label;
		private Label label2;
		private TextBox textNameEvent;
		private TextBox textIdEvent;
		private Button btnUpdateEvent;
		private Button btnDeleteEvent;
		private Label labelProcessTextEvent;
		private RichTextBox textDescriptionEvent;
		private Label label5;
		private Label label1;
		private TextBox textPriceEvent;
		private MonthCalendar eventCalendar;
		private Button btnClearEvent;
		private Button btnCreateEvent;
		private Label labelProcessTextEventStatus;
		private Label label6;
		private Button btnRefresh;
		private ComboBox textStatusEvent;
		private DateTimePicker textDateEvent;
	}
}