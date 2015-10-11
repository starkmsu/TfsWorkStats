using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsUtils.Accessors;

namespace TfsWorkStats
{
	internal class DataLoader
	{
		internal static List<WorkItem> GetBugs(
			string tfsUrl,
			List<string> areaPaths,
			DateTime from,
			DateTime to,
			Action<int> progressReportHandler)
		{
			return GetWorkItems(
				"Bug",
				null,
				tfsUrl,
				areaPaths,
				from,
				to,
				progressReportHandler);
		}

		internal static List<WorkItem> GetTasks(
			string tfsUrl,
			List<string> areaPaths,
			DateTime from,
			DateTime to,
			Action<int> progressReportHandler)
		{
			return GetWorkItems(
				"Task",
				"Development",
				tfsUrl,
				areaPaths,
				from,
				to,
				progressReportHandler);
		}

		internal static Dictionary<int, int> GetWrongAreaBugs(
			string tfsUrl,
			List<string> areaPaths,
			DateTime from,
			DateTime to,
			Action<int> progressReportHandler)
		{
			using (var wiqlAccessor = new TfsWiqlAccessor(tfsUrl))
			{
				var result = new Dictionary<int, int>();

				for(int i = 0; i < areaPaths.Count; ++i)
				{
					string areaPath = areaPaths[i];
					var notOurBugs = GetWrongAreaPathBugs(
						wiqlAccessor,
						areaPath,
						from,
						to,
						false,
						progressReportHandler == null
							? (Action<int>) null
							: x => progressReportHandler((100 * i  + x/2)/areaPaths.Count));

					foreach (var pair in notOurBugs)
					{
						result[pair.Key] = pair.Value;
					}

					var ourBugs = GetWrongAreaPathBugs(
						wiqlAccessor,
						areaPath,
						from,
						to,
						true,
						progressReportHandler == null
							? (Action<int>)null
							: x => progressReportHandler((100 * i + 50  + x/2)/areaPaths.Count));

					foreach (var pair in ourBugs)
					{
						result[pair.Key] = pair.Value;
					}
				}

				return result;
			}
		}

		private static List<WorkItem> GetWorkItems(
			string workItemType,
			string discipline,
			string tfsUrl,
			List<string> areaPaths,
			DateTime from,
			DateTime to,
			Action<int> progressReportHandler)
		{
			var strBuilder = new StringBuilder();
			strBuilder.Append("SELECT * FROM WorkItemLinks");
			strBuilder.Append(" WHERE Source.[System.WorkItemType] = '" + workItemType + "'");
			if (!string.IsNullOrEmpty(discipline))
				strBuilder.Append(" AND Source.[Discipline] = '" + discipline + "'");
			strBuilder.Append(" AND (Source.[System.State] = 'Closed' OR Source.[System.State] = 'Resolved')");
			strBuilder.Append(" AND Source.[Resolved Date] > '" + from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			strBuilder.Append("' AND Source.[Resolved Date] < '" + to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			strBuilder.Append("' AND (");
			for (int i = 0; i < areaPaths.Count; i++)
			{
				string areaPath = areaPaths[i];
				if (i > 0)
					strBuilder.Append(" OR ");
				strBuilder.Append("Source.[System.AreaPath] UNDER '" + areaPath + "'");
			}
			strBuilder.Append(")");
			strBuilder.Append(" AND Target.[System.WorkItemType] = 'Ship'");
			foreach (string areaPath in areaPaths)
			{
				strBuilder.Append(" AND Target.[System.AreaPath] NOT UNDER '" + areaPath + "'");
			}
			strBuilder.Append(" MODE (DoesNotContain)");

			using (var wiqlAccessor = new TfsWiqlAccessor(tfsUrl))
			{
				var bugsIds = wiqlAccessor.QueryIdsFromLinks(
					strBuilder.ToString(),
					null,
					null,
					null);
				if (bugsIds.Count == 0)
					return new List<WorkItem>(0);
				return wiqlAccessor.QueryWorkItemsByIds(
					bugsIds.Keys,
					null,
					progressReportHandler);
			}
		}

		private static Dictionary<int, int> GetWrongAreaPathBugs(
			TfsWiqlAccessor wiqlAccessor,
			string areaPath,
			DateTime from,
			DateTime to,
			bool ourBugs,
			Action<int> progressReportHandler)
		{
			var strBuilder = new StringBuilder();
			strBuilder.Append("SELECT [System.Id] FROM WorkItemLinks");
			strBuilder.Append(" WHERE Source.[System.WorkItemType] = 'Bug'");
			strBuilder.Append(" AND Source.[Resolved Date] > '" + from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			strBuilder.Append("' AND Source.[Resolved Date] < '" + to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			strBuilder.Append("' AND Source.[System.AreaPath] "
				+ (ourBugs ? "NOT UNDER '" : "UNDER '")
				+ areaPath
				+ "'");
			strBuilder.Append(" AND Target.[System.WorkItemType] = 'Ship'");
			strBuilder.Append(" AND Target.[System.AreaPath] "
				+ (ourBugs ? "UNDER '" : "NOT UNDER '")
				+ areaPath
				+ "'");
			strBuilder.Append(" MODE (MustContain)");

			var bugsWithOtherShips = wiqlAccessor.QueryIdsFromLinks(
				strBuilder.ToString(),
				null,
				null,
				progressReportHandler);

			if (bugsWithOtherShips.Count == 0)
				return new Dictionary<int, int>();

			strBuilder.Clear();
			strBuilder.Append("SELECT [System.Id] FROM WorkItemLinks");
			strBuilder.Append(" WHERE Source.[System.Id] IN (" + string.Join(",", bugsWithOtherShips.Keys) + ")");
			strBuilder.Append(" AND Target.[System.WorkItemType] = 'Ship'");
			strBuilder.Append(" AND Target.[System.AreaPath] "
				+ (ourBugs ? "NOT UNDER '" : "UNDER '")
				+ areaPath
				+ "'");
			strBuilder.Append(" MODE (DoesNotContain)");

			var result = wiqlAccessor.QueryIdsFromLinks(
				strBuilder.ToString(),
				null,
				null,
				null);

			return result.ToDictionary(i => i.Key, i => bugsWithOtherShips[i.Key].First());
		}
	}
}
