namespace BigDSignClientDesktop
{
    partial class SignGUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            labelProcessTextSign = new Label();
            listBoxSigns = new ListBox();
            labelProcessText = new Label();
            groupBox2 = new GroupBox();
            btnClear = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnCreate = new Button();
            label4 = new Label();
            label3 = new Label();
            textResolution = new TextBox();
            textSize = new TextBox();
            label = new Label();
            label2 = new Label();
            textLocation = new TextBox();
            textId = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(labelProcessTextSign);
            groupBox1.Controls.Add(listBoxSigns);
            groupBox1.Location = new Point(15, 6);
            groupBox1.Margin = new Padding(2, 1, 2, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 1, 2, 1);
            groupBox1.Size = new Size(439, 313);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "List Signs";
            // 
            // labelProcessTextSign
            // 
            labelProcessTextSign.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelProcessTextSign.AutoSize = true;
            labelProcessTextSign.Location = new Point(13, 282);
            labelProcessTextSign.Margin = new Padding(2, 0, 2, 0);
            labelProcessTextSign.Name = "labelProcessTextSign";
            labelProcessTextSign.Size = new Size(16, 15);
            labelProcessTextSign.TabIndex = 5;
            labelProcessTextSign.Text = "...";
            // 
            // listBoxSigns
            // 
            listBoxSigns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxSigns.FormattingEnabled = true;
            listBoxSigns.IntegralHeight = false;
            listBoxSigns.ItemHeight = 15;
            listBoxSigns.Location = new Point(13, 27);
            listBoxSigns.Margin = new Padding(2, 1, 2, 1);
            listBoxSigns.Name = "listBoxSigns";
            listBoxSigns.Size = new Size(411, 242);
            listBoxSigns.TabIndex = 1;
            listBoxSigns.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            listBoxSigns.MouseDoubleClick += ListBoxSigns_MouseDoubleClick;
            // 
            // labelProcessText
            // 
            labelProcessText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelProcessText.AutoSize = true;
            labelProcessText.Location = new Point(22, 254);
            labelProcessText.Margin = new Padding(2, 0, 2, 0);
            labelProcessText.Name = "labelProcessText";
            labelProcessText.Size = new Size(16, 15);
            labelProcessText.TabIndex = 2;
            labelProcessText.Text = "...";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(btnClear);
            groupBox2.Controls.Add(btnUpdate);
            groupBox2.Controls.Add(btnDelete);
            groupBox2.Controls.Add(labelProcessText);
            groupBox2.Controls.Add(btnCreate);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(textResolution);
            groupBox2.Controls.Add(textSize);
            groupBox2.Controls.Add(label);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textLocation);
            groupBox2.Controls.Add(textId);
            groupBox2.ImeMode = ImeMode.NoControl;
            groupBox2.Location = new Point(458, 6);
            groupBox2.Margin = new Padding(2, 1, 2, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 1, 2, 1);
            groupBox2.Size = new Size(378, 313);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Sign";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClear.Location = new Point(277, 277);
            btnClear.Margin = new Padding(2, 1, 2, 1);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(81, 24);
            btnClear.TabIndex = 11;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(107, 277);
            btnUpdate.Margin = new Padding(2, 1, 2, 1);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(81, 24);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += BtnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(192, 277);
            btnDelete.Margin = new Padding(2, 1, 2, 1);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(81, 24);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_ClickAsync;
            // 
            // btnCreate
            // 
            btnCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCreate.Location = new Point(22, 277);
            btnCreate.Margin = new Padding(2, 1, 2, 1);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(81, 24);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += BtnCreate_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 150);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(63, 15);
            label4.TabIndex = 8;
            label4.Text = "Resolution";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 203);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(27, 15);
            label3.TabIndex = 7;
            label3.Text = "Size";
            // 
            // textResolution
            // 
            textResolution.Location = new Point(22, 166);
            textResolution.Margin = new Padding(2, 1, 2, 1);
            textResolution.Name = "textResolution";
            textResolution.Size = new Size(251, 23);
            textResolution.TabIndex = 6;
            // 
            // textSize
            // 
            textSize.Location = new Point(22, 219);
            textSize.Margin = new Padding(2, 1, 2, 1);
            textSize.Name = "textSize";
            textSize.Size = new Size(251, 23);
            textSize.TabIndex = 5;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(22, 47);
            label.Margin = new Padding(2, 0, 2, 0);
            label.Name = "label";
            label.Size = new Size(17, 15);
            label.TabIndex = 4;
            label.Text = "Id";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 97);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 3;
            label2.Text = "Location";
            // 
            // textLocation
            // 
            textLocation.Location = new Point(22, 113);
            textLocation.Margin = new Padding(2, 1, 2, 1);
            textLocation.Name = "textLocation";
            textLocation.Size = new Size(251, 23);
            textLocation.TabIndex = 1;
            // 
            // textId
            // 
            textId.Enabled = false;
            textId.Location = new Point(22, 63);
            textId.Margin = new Padding(2, 1, 2, 1);
            textId.Name = "textId";
            textId.Size = new Size(251, 23);
            textId.TabIndex = 0;
            // 
            // SignGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(847, 334);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "SignGUI";
            FormClosing += SignGUI_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ListBox listBoxSigns;
        private GroupBox groupBox2;
        private Label label2;
        private Label labelProcessText;
        private TextBox textLocation;
        private TextBox textId;
        private TextBox textResolution;
        private TextBox textSize;
        private Label label;
        private Label label4;
        private Label label3;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnCreate;
        private Button btnClear;
        private Label labelProcessTextSign;
    }
}