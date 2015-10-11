using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsUtils.Const;
using TfsUtils.Parsers;

namespace TfsWorkStats
{
	public class StatsCalculator
	{
		internal static Dictionary<string, Dictionary<DateTime, Tuple<double, List<int>>>> ProcessWorkItems(IEnumerable<WorkItem> workItems)
		{
			var result = new Dictionary<string, Dictionary<DateTime, Tuple<double, List<int>>>>();
			foreach (var workItem in workItems)
			{
				double? allWork = workItem.Completed();
				if (!allWork.HasValue || allWork.Value == 0)
					continue;

				var tempDict = new Dictionary<string, double>();
				double workSum = 0;
				string prevState = "";
				foreach (Revision revision in workItem.Revisions)
				{
					var work = revision.Fields["Completed Work"].Value;
					if (work == null)
						continue;
					var user = (string)revision.Fields["Changed By"].Value;
					var workVal = (double)work;
					if (workVal > workSum)
					{
						double workTime = workVal - workSum;
						if (!tempDict.ContainsKey(user))
							tempDict.Add(user, 0);
						tempDict[user] += workTime;
						workSum = workVal;
					}
					var state = (string)revision.Fields["State"].Value;
					if (prevState != state
						&& state == WorkItemState.Resolved
						&& tempDict.Count > 0)
					{
						if (!tempDict.ContainsKey(user))
							user = tempDict.OrderBy(p => p.Value).Last().Key;
						var userCompleted = tempDict[user];
						tempDict.Clear();
						var changeDate = (DateTime)revision.Fields["Changed Date"].Value;

						if (!result.ContainsKey(user))
							result.Add(user, new Dictionary<DateTime, Tuple<double, List<int>>>());
						var userWork = result[user];
						if (!userWork.ContainsKey(changeDate.Date))
							userWork.Add(changeDate.Date, new Tuple<double, List<int>>(0, new List<int>()));
						var prevWork = userWork[changeDate.Date];
						prevWork.Item2.Add(workItem.Id);
						userWork[changeDate.Date] = new Tuple<double, List<int>>(prevWork.Item1 + userCompleted, prevWork.Item2);
					}
					prevState = state;
				}
			}

			var userToRemove = new List<string>(result.Count);
			foreach (var pair in result)
			{
				int resolveCount = pair.Value.Sum(pair2 => pair2.Value.Item2.Count);
				if (resolveCount <= 1)
					userToRemove.Add(pair.Key);
			}

			userToRemove.ForEach(u => result.Remove(u));

			return result;
		}
	}
}
