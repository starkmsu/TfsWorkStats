using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsWorkStats
{
	[Serializable]
	public class Config
	{
		public string TfsUrl { get; set; }

		public List<string> AreaPaths { get; set; }

		public List<string> AllAreaPaths { get; set; }

		public DateTime From { get; set; }

		public DateTime To { get; set; }

		public Config()
		{
			From = DateTime.Today.AddMonths(-1);
			To = DateTime.Today;
		}

		public Config Copy()
		{
			return new Config
			{
				TfsUrl = TfsUrl,
				AreaPaths = AreaPaths,
				AllAreaPaths = CopyIfNotNull(AllAreaPaths),
				From = From,
				To = To,
			};
		}

		public bool Equals(Config other)
		{
			if (other == null)
				return false;
			return TfsUrl == other.TfsUrl
				&& AreaPaths == other.AreaPaths
				&& CollectionsEquals(AllAreaPaths, other.AllAreaPaths)
				&& From.Date == other.From.Date
				&& To.Date == other.To.Date;
		}

		private List<T> CopyIfNotNull<T>(IEnumerable<T> target)
		{
			if (target == null)
				return null;
			return new List<T>(target.Distinct());
		}

		private bool CollectionsEquals<T>(List<T> first, List<T> second)
			where T : IEquatable<T>
		{
			if (first == null)
			{
				if (second == null || second.Count == 0)
					return true;
				return false;
			}
			if (second == null)
				return first.Count == 0;
			if (first.Count != second.Count)
				return false;
			return first.All(i => second.Any(arg => i.Equals(arg)))
				&& second.All(j => first.Any(arg => j.Equals(arg)));
		}
	}
}
