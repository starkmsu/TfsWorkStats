using System;

public class Class1
{
	public Class1()
	{
		internal static Dictionary<string, Dictionary<DateTime, Tuple<double, List<int>>>> ProcessBugs(IEnumerable<WorkItem> bugs)
		{
			var result = new Dictionary<string, Dictionary<DateTime, Tuple<double, List<int>>>>();
			foreach (var bug in bugs)
			{
				double? allWork = bug.Completed();
				if (!allWork.HasValue || allWork.Value == 0)
					continue;

				double workSum = 0;
				string prevState = "";
				foreach (Revision revision in bug.Revisions)
				{
					var changeDate = (DateTime)revision.Fields["Changed Date"].Value;
					var user = (string)revision.Fields["Changed By"].Value;
					var work = revision.Fields["Completed Work"].Value;
					var state = (string)revision.Fields["State"].Value;
					if (work == null)
						continue;
					var workVal = (double)work;
					if (workVal > workSum)
					{
						double workTime = workVal - workSum;
						if (!result.ContainsKey(user))
							result.Add(user, new Dictionary<DateTime, Tuple<double, List<int>>>());
						var userWork = result[user];
						if (!userWork.ContainsKey(changeDate.Date))
							userWork.Add(changeDate.Date, new Tuple<double, List<int>>(0,new List<int>()));
						var prevWork = userWork[changeDate.Date];
						if (prevState != state && state == WorkItemState.Resolved)
							prevWork.Item2.Add(bug.Id);
						userWork[changeDate.Date] = new Tuple<double, List<int>>(prevWork.Item1 + workTime, prevWork.Item2);
						workSum = workVal;
					}
					prevState = state;
				}
			}

			var userToRemove = new List<string>(result.Count);
			foreach (var pair in result)
			{
				if (pair.Value.Values.All(w => w.Item2.Count == 0))
					userToRemove.Add(pair.Key);
			}

			userToRemove.ForEach(u => result.Remove(u));

			return result;
		}
	}
}
