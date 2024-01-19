namespace BigDSignClientDesktop.GUI
{
    partial class StadiumGUI
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
            groupField = new GroupBox();
            label1 = new Label();
            textZipcode = new TextBox();
            btnClearS = new Button();
            btnUpdateS = new Button();
            btnDeleteS = new Button();
            btnCreateS = new Button();
            label4 = new Label();
            label3 = new Label();
            textStreet = new TextBox();
            textCity = new TextBox();
            label = new Label();
            lblStadiumName = new Label();
            textStadiumName = new TextBox();
            textId = new TextBox();
            groupList = new GroupBox();
            labelPTS = new Label();
            listBoxStadiums = new ListBox();
            groupField.SuspendLayout();
            groupList.SuspendLayout();
            SuspendLayout();
            // 
            // groupField
            // 
            groupField.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupField.Controls.Add(label1);
            groupField.Controls.Add(textZipcode);
            groupField.Controls.Add(btnClearS);
            groupField.Controls.Add(btnUpdateS);
            groupField.Controls.Add(btnDeleteS);
            groupField.Controls.Add(btnCreateS);
            groupField.Controls.Add(label4);
            groupField.Controls.Add(label3);
            groupField.Controls.Add(textStreet);
            groupField.Controls.Add(textCity);
            groupField.Controls.Add(label);
            groupField.Controls.Add(lblStadiumName);
            groupField.Controls.Add(textStadiumName);
            groupField.Controls.Add(textId);
            groupField.ImeMode = ImeMode.NoControl;
            groupField.Location = new Point(435, 10);
            groupField.Margin = new Padding(2, 1, 2, 1);
            groupField.Name = "groupField";
            groupField.Padding = new Padding(2, 1, 2, 1);
            groupField.Size = new Size(368, 401);
            groupField.TabIndex = 4;
            groupField.TabStop = false;
            groupField.Text = "Stadium";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 237);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 16;
            label1.Text = "Zipcode";
            // 
            // textZipcode
            // 
            textZipcode.Location = new Point(16, 253);
            textZipcode.Margin = new Padding(2, 1, 2, 1);
            textZipcode.Name = "textZipcode";
            textZipcode.Size = new Size(248, 23);
            textZipcode.TabIndex = 15;
            // 
            // btnClearS
            // 
            btnClearS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClearS.Location = new Point(267, 361);
            btnClearS.Margin = new Padding(2, 1, 2, 1);
            btnClearS.Name = "btnClearS";
            btnClearS.Size = new Size(81, 24);
            btnClearS.TabIndex = 14;
            btnClearS.Text = "Clear";
            btnClearS.UseVisualStyleBackColor = true;
            btnClearS.Click += BtnClearS_Click;
            // 
            // btnUpdateS
            // 
            btnUpdateS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdateS.Location = new Point(100, 361);
            btnUpdateS.Margin = new Padding(2, 1, 2, 1);
            btnUpdateS.Name = "btnUpdateS";
            btnUpdateS.Size = new Size(81, 24);
            btnUpdateS.TabIndex = 13;
            btnUpdateS.Text = "Update";
            btnUpdateS.UseVisualStyleBackColor = true;
            btnUpdateS.Click += BtnUpdateS_Click;
            // 
            // btnDeleteS
            // 
            btnDeleteS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDeleteS.Location = new Point(183, 361);
            btnDeleteS.Margin = new Padding(2, 1, 2, 1);
            btnDeleteS.Name = "btnDeleteS";
            btnDeleteS.Size = new Size(81, 24);
            btnDeleteS.TabIndex = 12;
            btnDeleteS.Text = "Delete";
            btnDeleteS.UseVisualStyleBackColor = true;
            btnDeleteS.Click += BtnDeleteS_Click;
            // 
            // btnCreateS
            // 
            btnCreateS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCreateS.Location = new Point(15, 361);
            btnCreateS.Margin = new Padding(2, 1, 2, 1);
            btnCreateS.Name = "btnCreateS";
            btnCreateS.Size = new Size(81, 24);
            btnCreateS.TabIndex = 11;
            btnCreateS.Text = "Create";
            btnCreateS.UseVisualStyleBackColor = true;
            btnCreateS.Click += BtnCreateS_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 131);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 8;
            label4.Text = "Street";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 182);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(28, 15);
            label3.TabIndex = 7;
            label3.Text = "City";
            // 
            // textStreet
            // 
            textStreet.Location = new Point(16, 147);
            textStreet.Margin = new Padding(2, 1, 2, 1);
            textStreet.Name = "textStreet";
            textStreet.Size = new Size(248, 23);
            textStreet.TabIndex = 6;
            // 
            // textCity
            // 
            textCity.Location = new Point(16, 200);
            textCity.Margin = new Padding(2, 1, 2, 1);
            textCity.Name = "textCity";
            textCity.Size = new Size(248, 23);
            textCity.TabIndex = 5;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(15, 26);
            label.Margin = new Padding(2, 0, 2, 0);
            label.Name = "label";
            label.Size = new Size(17, 15);
            label.TabIndex = 4;
            label.Text = "Id";
            // 
            // lblStadiumName
            // 
            lblStadiumName.AutoSize = true;
            lblStadiumName.Location = new Point(15, 76);
            lblStadiumName.Margin = new Padding(2, 0, 2, 0);
            lblStadiumName.Name = "lblStadiumName";
            lblStadiumName.Size = new Size(86, 15);
            lblStadiumName.TabIndex = 3;
            lblStadiumName.Text = "Stadium Name";
            // 
            // textStadiumName
            // 
            textStadiumName.Location = new Point(16, 92);
            textStadiumName.Margin = new Padding(2, 1, 2, 1);
            textStadiumName.Name = "textStadiumName";
            textStadiumName.Size = new Size(248, 23);
            textStadiumName.TabIndex = 1;
            // 
            // textId
            // 
            textId.Enabled = false;
            textId.Location = new Point(16, 44);
            textId.Margin = new Padding(2, 1, 2, 1);
            textId.Name = "textId";
            textId.Size = new Size(248, 23);
            textId.TabIndex = 0;
            // 
            // groupList
            // 
            groupList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupList.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupList.Controls.Add(labelPTS);
            groupList.Controls.Add(listBoxStadiums);
            groupList.Location = new Point(11, 10);
            groupList.Margin = new Padding(2, 1, 2, 1);
            groupList.Name = "groupList";
            groupList.Padding = new Padding(2, 1, 2, 1);
            groupList.Size = new Size(419, 401);
            groupList.TabIndex = 3;
            groupList.TabStop = false;
            groupList.Text = "List Stadiums";
            groupList.Enter += groupList_Enter;
            // 
            // labelPTS
            // 
            labelPTS.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelPTS.AutoSize = true;
            labelPTS.Location = new Point(17, 366);
            labelPTS.Margin = new Padding(2, 0, 2, 0);
            labelPTS.Name = "labelPTS";
            labelPTS.Size = new Size(16, 15);
            labelPTS.TabIndex = 7;
            labelPTS.Text = "...";
            // 
            // listBoxStadiums
            // 
            listBoxStadiums.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxStadiums.FormattingEnabled = true;
            listBoxStadiums.IntegralHeight = false;
            listBoxStadiums.ItemHeight = 15;
            listBoxStadiums.Location = new Point(17, 25);
            listBoxStadiums.Margin = new Padding(2, 1, 2, 1);
            listBoxStadiums.Name = "listBoxStadiums";
            listBoxStadiums.Size = new Size(378, 340);
            listBoxStadiums.TabIndex = 1;
            listBoxStadiums.SelectedIndexChanged += ListBoxStadiums_SelectedIndexChanged;
            listBoxStadiums.MouseDoubleClick += ListBoxStadiums_MouseDoubleClick;
            // 
            // StadiumGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(813, 418);
            Controls.Add(groupField);
            Controls.Add(groupList);
            Name = "StadiumGUI";
            Text = "Stadium Management";
            groupField.ResumeLayout(false);
            groupField.PerformLayout();
            groupList.ResumeLayout(false);
            groupList.PerformLayout();
            ResumeLayout(false);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupList_Enter(object sender, EventArgs e)
        {

        }


        #endregion

        private GroupBox groupField;
        private Label label4;
        private Label label3;
        private TextBox textStreet;
        private TextBox textCity;
        private Label label;
        private Label lblStadiumName;
        private TextBox textStadiumName;
        private TextBox textId;
        private GroupBox groupList;
        private ListBox listBoxStadiums;
        private Button btnUpdateS;
        private Button btnDeleteS;
        private Button btnCreateS;
        private Label labelPTS;
        private Button btnClearS;
        private Label label1;
        private TextBox textZipcode;
    }
}