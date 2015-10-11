﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsWorkStats
{
	public partial class MainForm : Form
	{
		private const string ZeroPercents = "0%";

		private readonly Config m_config;

		private List<WorkItem> m_bugs;
		private List<WorkItem> m_tasks;
		private Dictionary<int, int> m_wrongAreaBugs;
		private Dictionary<string, Dictionary<DateTime, Tuple<double, List<int>>>> m_bugsStats;
		private Dictionary<string, Dictionary<DateTime, Tuple<double, List<int>>>> m_tasksStats;

		private int m_hoveredIndex;

		public MainForm()
		{
			InitializeComponent();

			m_config = ConfigManager.LoadConfig();

			LoadSettings();
		}

		private void MainFormClosing(object sender, FormClosingEventArgs e)
		{
			ConfigManager.SaveConfig(m_config);
		}

		private void AppendExceptionString(Exception exc, StringBuilder stringBuilder)
		{
			if (exc.InnerException != null)
				AppendExceptionString(exc.InnerException, stringBuilder);
			stringBuilder.AppendLine(exc.Message);
			stringBuilder.AppendLine(exc.StackTrace);
		}

		private void HandleException(
			Exception exc,
			string caption)
		{
			var strBuilder = new StringBuilder();
			AppendExceptionString(exc, strBuilder);
			string text = strBuilder.ToString();
			using (var fileWriter = new StreamWriter(DateTime.Now.ToString("yyyy-mm-dd HH-mm-ss") + ".txt", false))
			{
				fileWriter.WriteLine(text);
			}
			Invoke(new Action(() => MessageBox.Show(text, caption)));
		}

		private void ProgressReport(int percent, Label label)
		{
			string percentStr = percent + "%";
			if (label.Text == percentStr)
				return;
			if (InvokeRequired)
				Invoke(new Action(() => label.Text = percentStr));
			else
				label.Text = percentStr;
		}

		private void LoadSettings()
		{
			tfsUrlTextBox.Text = m_config.TfsUrl;
			if (m_config.AllAreaPaths != null && m_config.AllAreaPaths.Count > 0)
			{
				areaPathComboBox.DataSource = m_config.AllAreaPaths;
				areaPathComboBox.Text = m_config.AllAreaPaths[0];
				taskAreaPathComboBox.DataSource = m_config.AllAreaPaths;
				taskAreaPathComboBox.Text = m_config.AllAreaPaths[0];
			}
			if (m_config.AreaPaths != null)
				m_config.AreaPaths.ForEach(a => areaPathListBox.Items.Add(a));
			if (m_config.TaskAreaPaths != null)
				m_config.TaskAreaPaths.ForEach(a => taskAreaPathListBox.Items.Add(a));
			fromDateTimePicker.Value = m_config.From;
			toDateTimePicker.Value = m_config.To;
			taskFromDateTimePicker.Value = m_config.From;
			taskToDateTimePicker.Value = m_config.To;
			if (areaPathListBox.Items.Count > 0)
				checkWrongAreaButton.Enabled = true;
		}

		private void SaveSettingsToConfig()
		{
			m_config.TfsUrl = tfsUrlTextBox.Text;
			m_config.AreaPaths = areaPathListBox.Items.Cast<string>().ToList();
			m_config.TaskAreaPaths = taskAreaPathListBox.Items.Cast<string>().ToList();
			m_config.AllAreaPaths = m_config.AllAreaPaths == null
				? m_config.AreaPaths
				: m_config.AllAreaPaths.Concat(m_config.AreaPaths).Distinct().ToList();
			m_config.From = fromDateTimePicker.Value;
			m_config.To = toDateTimePicker.Value;
		}

		private void LoadBugsDataButtonClick(object sender, EventArgs e)
		{
			checkWrongAreaButton.Enabled = false;
			loadBugsDataButton.Enabled = false;
			resultsGroupBox.Enabled = false;
			bugsDataPercentLabel.Text = ZeroPercents;
			bugsDataPercentLabel.Visible = true;

			ThreadPool.QueueUserWorkItem(x => LoadBugsData());
		}

		private void LoadBugsData()
		{
			try
			{
				m_bugs = DataLoader.GetBugs(
					tfsUrlTextBox.Text,
					areaPathListBox.Items.Cast<string>().ToList(),
					fromDateTimePicker.Value,
					toDateTimePicker.Value,
					x => ProgressReport(x, bugsDataPercentLabel));
				m_bugsStats = StatsCalculator.ProcessWorkItems(m_bugs);
				Invoke(new Action(() =>
				{
					resultsGroupBox.Enabled = true;
					var users = m_bugsStats.Keys.ToList();
					users.Sort();
					workersComboBox.DataSource = users;
					if (users.Count > 0)
						workersComboBox.SelectedIndex = 0;
				}));
			}
			catch (Exception e)
			{
				HandleException(e, "Error");
			}
			Invoke(new Action(() =>
				{
					bugsDataPercentLabel.Visible = false;
					loadBugsDataButton.Enabled = true;
					checkWrongAreaButton.Enabled = true;
				}));
		}

		private void AreaPathAddButtonClick(object sender, EventArgs e)
		{
			string first = areaPathComboBox.Text;
			List<string> list = m_config.AllAreaPaths;
			if (!list.Contains(first))
			{
				list.Add(first);
				areaPathComboBox.DataSource = list;
			}
			if (areaPathListBox.Items.Contains(first))
				return;
			areaPathListBox.Items.Add(first);
			checkWrongAreaButton.Enabled = true;
		}

		private void AreaPathRemoveButtonClick(object sender, EventArgs e)
		{
			areaPathListBox.Items.Remove(areaPathListBox.SelectedItem);
			areaPathListBox.Enabled = false;
			if (areaPathListBox.Items.Count == 0)
				checkWrongAreaButton.Enabled = false;
		}

		private void AreaPathListBoxSelectedValueChanged(object sender, EventArgs e)
		{
			if (areaPathListBox.SelectedItem != null
				&& !areaPathRemoveButton.Enabled)
				areaPathRemoveButton.Enabled = true;
		}

		private void CheckWrongAreaButtonClick(object sender, EventArgs e)
		{
			checkWrongAreaButton.Enabled = false;
			checkWrongAreaBugsPercentLabel.Text = ZeroPercents;
			checkWrongAreaBugsPercentLabel.Visible = true;

			ThreadPool.QueueUserWorkItem(x => SearchWrongAreaBugs());
		}

		private void SearchWrongAreaBugs()
		{
			try
			{
				m_wrongAreaBugs = DataLoader.GetWrongAreaBugs(
					tfsUrlTextBox.Text,
					areaPathListBox.Items.Cast<string>().ToList(),
					fromDateTimePicker.Value,
					toDateTimePicker.Value,
					x => ProgressReport(x, checkWrongAreaBugsPercentLabel));
				SaveSettingsToConfig();
				Invoke(new Action(() =>
				{
					wrongAreaLabel.Text = m_wrongAreaBugs.Count.ToString(CultureInfo.InvariantCulture);
					if (m_wrongAreaBugs.Count > 0)
						fixWrongAreaBugsButton.Visible = true;
					else
						loadBugsDataButton.Enabled = true;
				}));
			}
			catch (Exception e)
			{
				HandleException(e, "Error");
			}
			Invoke(new Action(() =>
				{
					checkWrongAreaBugsPercentLabel.Visible = false;
					checkWrongAreaButton.Enabled = true;
				}));
		}

		private void FixWrongAreaBugsButtonClick(object sender, EventArgs e)
		{
			fixWrongAreaBugsButton.Enabled = false;
			fixPercentLabel.Text = ZeroPercents;
			fixPercentLabel.Visible = true;

			ThreadPool.QueueUserWorkItem(x => FixBugs());
		}

		private void FixBugs()
		{
			try
			{
				DataUploader.FixBugsAreaPaths(
					m_config.TfsUrl,
					m_wrongAreaBugs,
					x => ProgressReport(x, fixPercentLabel));
				Invoke(new Action(() =>
				{
					fixWrongAreaBugsButton.Visible = false;
					wrongAreaLabel.Text = @"0";
					loadBugsDataButton.Enabled = true;
				}));
			}
			catch (Exception e)
			{
				HandleException(e, "Error");
			}
			Invoke(new Action(() =>
				{
					fixPercentLabel.Visible = false;
					fixWrongAreaBugsButton.Enabled = true;
				}));
		}

		private void WorkersComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			string user = workersComboBox.Text;
			if (string.IsNullOrEmpty(user))
				return;

			var userWork = m_bugsStats[user];
			resultsListBox.Items.Clear();
			foreach (var date in userWork.Keys.OrderBy(i => i))
			{
				var dayWork = userWork[date];
				string str = date.ToString("dd-MM-yyyy") + ": "
					+ dayWork.Item1 + " hours - resolved "
					+ dayWork.Item2.Count + ". Ave = ";
				if (dayWork.Item2.Count == 0)
					str += @"N\A";
				else if (dayWork.Item1/dayWork.Item2.Count - Math.Floor(dayWork.Item1/dayWork.Item2.Count) > 0)
					str += (dayWork.Item1/dayWork.Item2.Count).ToString("N", CultureInfo.InvariantCulture);
				else
					str += (dayWork.Item1/dayWork.Item2.Count).ToString(CultureInfo.InvariantCulture);
				resultsListBox.Items.Add(str);
			}
		}

		private void ResultsListBoxMouseMove(object sender, MouseEventArgs e)
		{
			int newHoveredIndex = resultsListBox.IndexFromPoint(e.Location);

			if (m_hoveredIndex == newHoveredIndex)
				return;

			m_hoveredIndex = newHoveredIndex;
			if (m_hoveredIndex <= -1)
			{
				if (toolTip1.Active)
					toolTip1.Active = false;
				return;
			}
			string info = resultsListBox.Items[m_hoveredIndex].ToString();
			string user = workersComboBox.Text;
			int ind = info.IndexOf(':');
			var dateStr = info.Substring(0, ind);
			DateTime date = DateTime.Parse(dateStr);
			var userWork = m_bugsStats[user];
			string description = string.Join(",", userWork[date].Item2);

			toolTip1.Active = false;
			toolTip1.SetToolTip(resultsListBox, description);
			toolTip1.Active = true;
		}

		private void TaskAreaPathAddButtonClick(object sender, EventArgs e)
		{
			string first = taskAreaPathComboBox.Text;
			List<string> list = m_config.AllAreaPaths;
			if (!list.Contains(first))
			{
				list.Add(first);
				taskAreaPathComboBox.DataSource = list;
			}
			if (taskAreaPathListBox.Items.Contains(first))
				return;
			taskAreaPathListBox.Items.Add(first);
			loadTasksDataButton.Enabled = true;
		}

		private void TaskAreaPathRemoveButtonClick(object sender, EventArgs e)
		{
			taskAreaPathListBox.Items.Remove(taskAreaPathListBox.SelectedItem);
			taskAreaPathListBox.Enabled = false;
		}

		private void TaskAreaPathListBoxSelectedValueChanged(object sender, EventArgs e)
		{
			if (taskAreaPathListBox.SelectedItem != null
				&& !taskAreaPathRemoveButton.Enabled)
				taskAreaPathRemoveButton.Enabled = true;
		}

		private void LoadTasksDataButtonClick(object sender, EventArgs e)
		{
			loadTasksDataButton.Enabled = false;
			taskResultsGroupBox.Enabled = false;
			tasksDataPercentLabel.Text = ZeroPercents;
			tasksDataPercentLabel.Visible = true;

			ThreadPool.QueueUserWorkItem(x => LoadTasksData());
		}

		private void LoadTasksData()
		{
			try
			{
				m_tasks = DataLoader.GetTasks(
					tfsUrlTextBox.Text,
					taskAreaPathListBox.Items.Cast<string>().ToList(),
					taskFromDateTimePicker.Value,
					taskToDateTimePicker.Value,
					x => ProgressReport(x, tasksDataPercentLabel));
				m_tasksStats = StatsCalculator.ProcessWorkItems(m_tasks);
				Invoke(new Action(() =>
				{
					taskResultsGroupBox.Enabled = true;
					var users = m_tasksStats.Keys.ToList();
					users.Sort();
					taskWorkersComboBox.DataSource = users;
					if (users.Count > 0)
						taskWorkersComboBox.SelectedIndex = 0;
				}));
			}
			catch (Exception e)
			{
				HandleException(e, "Error");
			}
			Invoke(new Action(() =>
				{
					tasksDataPercentLabel.Visible = false;
					loadTasksDataButton.Enabled = true;
				}));
		}

		private void TaskWorkersComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			string user = taskWorkersComboBox.Text;
			if (string.IsNullOrEmpty(user))
				return;

			var userWork = m_tasksStats[user];
			taskResultsListBox.Items.Clear();
			foreach (var date in userWork.Keys.OrderBy(i => i))
			{
				var dayWork = userWork[date];
				string str = date.ToString("dd-MM-yyyy") + ": "
					+ dayWork.Item1 + " hours - resolved "
					+ dayWork.Item2.Count + ". Ave = ";
				if (dayWork.Item2.Count == 0)
					str += @"N\A";
				else if (dayWork.Item1 / dayWork.Item2.Count - Math.Floor(dayWork.Item1 / dayWork.Item2.Count) > 0)
					str += (dayWork.Item1 / dayWork.Item2.Count).ToString("N", CultureInfo.InvariantCulture);
				else
					str += (dayWork.Item1 / dayWork.Item2.Count).ToString(CultureInfo.InvariantCulture);
				taskResultsListBox.Items.Add(str);
			}
		}
	}
}
