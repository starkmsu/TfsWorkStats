namespace TfsWorkStats
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.bugsTab = new System.Windows.Forms.TabPage();
			this.resultsGroupBox = new System.Windows.Forms.GroupBox();
			this.resultsListBox = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.workersComboBox = new System.Windows.Forms.ComboBox();
			this.checkWrongAreaBugsPercentLabel = new System.Windows.Forms.Label();
			this.fixPercentLabel = new System.Windows.Forms.Label();
			this.fixWrongAreaBugsButton = new System.Windows.Forms.Button();
			this.wrongAreaLabel = new System.Windows.Forms.Label();
			this.checkWrongAreaButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.areaPathListBox = new System.Windows.Forms.ListBox();
			this.areaPathComboBox = new System.Windows.Forms.ComboBox();
			this.areaPathRemoveButton = new System.Windows.Forms.Button();
			this.areaPathAddButton = new System.Windows.Forms.Button();
			this.bugsDataPercentLabel = new System.Windows.Forms.Label();
			this.loadBugsDataButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tasksTab = new System.Windows.Forms.TabPage();
			this.settingsTab = new System.Windows.Forms.TabPage();
			this.tfsUrlTextBox = new System.Windows.Forms.TextBox();
			this.tfsUrlLabel = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.bugsTab.SuspendLayout();
			this.resultsGroupBox.SuspendLayout();
			this.settingsTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.bugsTab);
			this.tabControl1.Controls.Add(this.tasksTab);
			this.tabControl1.Controls.Add(this.settingsTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(696, 404);
			this.tabControl1.TabIndex = 0;
			// 
			// bugsTab
			// 
			this.bugsTab.Controls.Add(this.resultsGroupBox);
			this.bugsTab.Controls.Add(this.checkWrongAreaBugsPercentLabel);
			this.bugsTab.Controls.Add(this.fixPercentLabel);
			this.bugsTab.Controls.Add(this.fixWrongAreaBugsButton);
			this.bugsTab.Controls.Add(this.wrongAreaLabel);
			this.bugsTab.Controls.Add(this.checkWrongAreaButton);
			this.bugsTab.Controls.Add(this.label3);
			this.bugsTab.Controls.Add(this.toDateTimePicker);
			this.bugsTab.Controls.Add(this.label2);
			this.bugsTab.Controls.Add(this.fromDateTimePicker);
			this.bugsTab.Controls.Add(this.areaPathListBox);
			this.bugsTab.Controls.Add(this.areaPathComboBox);
			this.bugsTab.Controls.Add(this.areaPathRemoveButton);
			this.bugsTab.Controls.Add(this.areaPathAddButton);
			this.bugsTab.Controls.Add(this.bugsDataPercentLabel);
			this.bugsTab.Controls.Add(this.loadBugsDataButton);
			this.bugsTab.Controls.Add(this.label1);
			this.bugsTab.Location = new System.Drawing.Point(4, 22);
			this.bugsTab.Name = "bugsTab";
			this.bugsTab.Padding = new System.Windows.Forms.Padding(3);
			this.bugsTab.Size = new System.Drawing.Size(688, 378);
			this.bugsTab.TabIndex = 0;
			this.bugsTab.Text = "Bugs";
			this.bugsTab.UseVisualStyleBackColor = true;
			// 
			// resultsGroupBox
			// 
			this.resultsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resultsGroupBox.Controls.Add(this.resultsListBox);
			this.resultsGroupBox.Controls.Add(this.label4);
			this.resultsGroupBox.Controls.Add(this.workersComboBox);
			this.resultsGroupBox.Enabled = false;
			this.resultsGroupBox.Location = new System.Drawing.Point(8, 195);
			this.resultsGroupBox.Name = "resultsGroupBox";
			this.resultsGroupBox.Size = new System.Drawing.Size(672, 180);
			this.resultsGroupBox.TabIndex = 31;
			this.resultsGroupBox.TabStop = false;
			this.resultsGroupBox.Text = "Results";
			// 
			// resultsListBox
			// 
			this.resultsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resultsListBox.FormattingEnabled = true;
			this.resultsListBox.Location = new System.Drawing.Point(6, 46);
			this.resultsListBox.Name = "resultsListBox";
			this.resultsListBox.Size = new System.Drawing.Size(660, 121);
			this.resultsListBox.TabIndex = 25;
			this.resultsListBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ResultsListBoxMouseMove);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 24;
			this.label4.Text = "Worker:";
			// 
			// workersComboBox
			// 
			this.workersComboBox.FormattingEnabled = true;
			this.workersComboBox.Location = new System.Drawing.Point(53, 19);
			this.workersComboBox.Name = "workersComboBox";
			this.workersComboBox.Size = new System.Drawing.Size(216, 21);
			this.workersComboBox.TabIndex = 0;
			this.workersComboBox.SelectedIndexChanged += new System.EventHandler(this.WorkersComboBoxSelectedIndexChanged);
			// 
			// checkWrongAreaBugsPercentLabel
			// 
			this.checkWrongAreaBugsPercentLabel.AutoSize = true;
			this.checkWrongAreaBugsPercentLabel.Location = new System.Drawing.Point(117, 171);
			this.checkWrongAreaBugsPercentLabel.Name = "checkWrongAreaBugsPercentLabel";
			this.checkWrongAreaBugsPercentLabel.Size = new System.Drawing.Size(21, 13);
			this.checkWrongAreaBugsPercentLabel.TabIndex = 30;
			this.checkWrongAreaBugsPercentLabel.Text = "0%";
			this.checkWrongAreaBugsPercentLabel.Visible = false;
			// 
			// fixPercentLabel
			// 
			this.fixPercentLabel.AutoSize = true;
			this.fixPercentLabel.Location = new System.Drawing.Point(283, 171);
			this.fixPercentLabel.Name = "fixPercentLabel";
			this.fixPercentLabel.Size = new System.Drawing.Size(21, 13);
			this.fixPercentLabel.TabIndex = 29;
			this.fixPercentLabel.Text = "0%";
			this.fixPercentLabel.Visible = false;
			// 
			// fixWrongAreaBugsButton
			// 
			this.fixWrongAreaBugsButton.Location = new System.Drawing.Point(175, 166);
			this.fixWrongAreaBugsButton.Name = "fixWrongAreaBugsButton";
			this.fixWrongAreaBugsButton.Size = new System.Drawing.Size(102, 23);
			this.fixWrongAreaBugsButton.TabIndex = 28;
			this.fixWrongAreaBugsButton.Text = "Fix wrong area!";
			this.fixWrongAreaBugsButton.UseVisualStyleBackColor = true;
			this.fixWrongAreaBugsButton.Visible = false;
			this.fixWrongAreaBugsButton.Click += new System.EventHandler(this.FixWrongAreaBugsButtonClick);
			// 
			// wrongAreaLabel
			// 
			this.wrongAreaLabel.AutoSize = true;
			this.wrongAreaLabel.Location = new System.Drawing.Point(144, 171);
			this.wrongAreaLabel.Name = "wrongAreaLabel";
			this.wrongAreaLabel.Size = new System.Drawing.Size(25, 13);
			this.wrongAreaLabel.TabIndex = 27;
			this.wrongAreaLabel.Text = "???";
			// 
			// checkWrongAreaButton
			// 
			this.checkWrongAreaButton.Enabled = false;
			this.checkWrongAreaButton.Location = new System.Drawing.Point(8, 166);
			this.checkWrongAreaButton.Name = "checkWrongAreaButton";
			this.checkWrongAreaButton.Size = new System.Drawing.Size(102, 23);
			this.checkWrongAreaButton.TabIndex = 26;
			this.checkWrongAreaButton.Text = "Check wrong area";
			this.checkWrongAreaButton.UseVisualStyleBackColor = true;
			this.checkWrongAreaButton.Click += new System.EventHandler(this.CheckWrongAreaButtonClick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(159, 143);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(23, 13);
			this.label3.TabIndex = 25;
			this.label3.Text = "To:";
			// 
			// toDateTimePicker
			// 
			this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.toDateTimePicker.Location = new System.Drawing.Point(188, 140);
			this.toDateTimePicker.Name = "toDateTimePicker";
			this.toDateTimePicker.Size = new System.Drawing.Size(91, 20);
			this.toDateTimePicker.TabIndex = 24;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 23;
			this.label2.Text = "From:";
			// 
			// fromDateTimePicker
			// 
			this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.fromDateTimePicker.Location = new System.Drawing.Point(47, 140);
			this.fromDateTimePicker.Name = "fromDateTimePicker";
			this.fromDateTimePicker.Size = new System.Drawing.Size(91, 20);
			this.fromDateTimePicker.TabIndex = 22;
			// 
			// areaPathListBox
			// 
			this.areaPathListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.areaPathListBox.FormattingEnabled = true;
			this.areaPathListBox.Location = new System.Drawing.Point(8, 39);
			this.areaPathListBox.Name = "areaPathListBox";
			this.areaPathListBox.Size = new System.Drawing.Size(672, 95);
			this.areaPathListBox.TabIndex = 21;
			this.areaPathListBox.SelectedValueChanged += new System.EventHandler(this.AreaPathListBoxSelectedValueChanged);
			// 
			// areaPathComboBox
			// 
			this.areaPathComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.areaPathComboBox.FormattingEnabled = true;
			this.areaPathComboBox.Location = new System.Drawing.Point(68, 12);
			this.areaPathComboBox.Name = "areaPathComboBox";
			this.areaPathComboBox.Size = new System.Drawing.Size(468, 21);
			this.areaPathComboBox.TabIndex = 20;
			// 
			// areaPathRemoveButton
			// 
			this.areaPathRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.areaPathRemoveButton.Enabled = false;
			this.areaPathRemoveButton.Location = new System.Drawing.Point(613, 10);
			this.areaPathRemoveButton.Name = "areaPathRemoveButton";
			this.areaPathRemoveButton.Size = new System.Drawing.Size(69, 23);
			this.areaPathRemoveButton.TabIndex = 19;
			this.areaPathRemoveButton.Text = "Remove";
			this.areaPathRemoveButton.UseVisualStyleBackColor = true;
			this.areaPathRemoveButton.Click += new System.EventHandler(this.AreaPathRemoveButtonClick);
			// 
			// areaPathAddButton
			// 
			this.areaPathAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.areaPathAddButton.Location = new System.Drawing.Point(545, 10);
			this.areaPathAddButton.Name = "areaPathAddButton";
			this.areaPathAddButton.Size = new System.Drawing.Size(62, 23);
			this.areaPathAddButton.TabIndex = 18;
			this.areaPathAddButton.Text = "Add";
			this.areaPathAddButton.UseVisualStyleBackColor = true;
			this.areaPathAddButton.Click += new System.EventHandler(this.AreaPathAddButtonClick);
			// 
			// bugsDataPercentLabel
			// 
			this.bugsDataPercentLabel.AutoSize = true;
			this.bugsDataPercentLabel.Location = new System.Drawing.Point(391, 171);
			this.bugsDataPercentLabel.Name = "bugsDataPercentLabel";
			this.bugsDataPercentLabel.Size = new System.Drawing.Size(21, 13);
			this.bugsDataPercentLabel.TabIndex = 17;
			this.bugsDataPercentLabel.Text = "0%";
			this.bugsDataPercentLabel.Visible = false;
			// 
			// loadBugsDataButton
			// 
			this.loadBugsDataButton.Enabled = false;
			this.loadBugsDataButton.Location = new System.Drawing.Point(310, 166);
			this.loadBugsDataButton.Name = "loadBugsDataButton";
			this.loadBugsDataButton.Size = new System.Drawing.Size(75, 23);
			this.loadBugsDataButton.TabIndex = 14;
			this.loadBugsDataButton.Text = "Load data";
			this.loadBugsDataButton.UseVisualStyleBackColor = true;
			this.loadBugsDataButton.Click += new System.EventHandler(this.LoadBugsDataButtonClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "AreaPath:";
			// 
			// tasksTab
			// 
			this.tasksTab.Location = new System.Drawing.Point(4, 22);
			this.tasksTab.Name = "tasksTab";
			this.tasksTab.Padding = new System.Windows.Forms.Padding(3);
			this.tasksTab.Size = new System.Drawing.Size(688, 378);
			this.tasksTab.TabIndex = 1;
			this.tasksTab.Text = "Tasks";
			this.tasksTab.UseVisualStyleBackColor = true;
			// 
			// settingsTab
			// 
			this.settingsTab.Controls.Add(this.tfsUrlTextBox);
			this.settingsTab.Controls.Add(this.tfsUrlLabel);
			this.settingsTab.Location = new System.Drawing.Point(4, 22);
			this.settingsTab.Name = "settingsTab";
			this.settingsTab.Size = new System.Drawing.Size(688, 378);
			this.settingsTab.TabIndex = 2;
			this.settingsTab.Text = "Settings";
			this.settingsTab.UseVisualStyleBackColor = true;
			// 
			// tfsUrlTextBox
			// 
			this.tfsUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tfsUrlTextBox.Location = new System.Drawing.Point(65, 12);
			this.tfsUrlTextBox.Name = "tfsUrlTextBox";
			this.tfsUrlTextBox.Size = new System.Drawing.Size(512, 20);
			this.tfsUrlTextBox.TabIndex = 15;
			// 
			// tfsUrlLabel
			// 
			this.tfsUrlLabel.AutoSize = true;
			this.tfsUrlLabel.Location = new System.Drawing.Point(8, 15);
			this.tfsUrlLabel.Name = "tfsUrlLabel";
			this.tfsUrlLabel.Size = new System.Drawing.Size(44, 13);
			this.tfsUrlLabel.TabIndex = 14;
			this.tfsUrlLabel.Text = "TFS url:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(696, 404);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainForm";
			this.Text = "WorkStats";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
			this.tabControl1.ResumeLayout(false);
			this.bugsTab.ResumeLayout(false);
			this.bugsTab.PerformLayout();
			this.resultsGroupBox.ResumeLayout(false);
			this.resultsGroupBox.PerformLayout();
			this.settingsTab.ResumeLayout(false);
			this.settingsTab.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage bugsTab;
		private System.Windows.Forms.TabPage tasksTab;
		private System.Windows.Forms.TabPage settingsTab;
		private System.Windows.Forms.TextBox tfsUrlTextBox;
		private System.Windows.Forms.Label tfsUrlLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button loadBugsDataButton;
		private System.Windows.Forms.Label bugsDataPercentLabel;
		private System.Windows.Forms.ListBox areaPathListBox;
		private System.Windows.Forms.ComboBox areaPathComboBox;
		private System.Windows.Forms.Button areaPathRemoveButton;
		private System.Windows.Forms.Button areaPathAddButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker fromDateTimePicker;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker toDateTimePicker;
		private System.Windows.Forms.Button checkWrongAreaButton;
		private System.Windows.Forms.Label wrongAreaLabel;
		private System.Windows.Forms.Button fixWrongAreaBugsButton;
		private System.Windows.Forms.Label fixPercentLabel;
		private System.Windows.Forms.Label checkWrongAreaBugsPercentLabel;
		private System.Windows.Forms.GroupBox resultsGroupBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox workersComboBox;
		private System.Windows.Forms.ListBox resultsListBox;
		private System.Windows.Forms.ToolTip toolTip1;

	}
}

