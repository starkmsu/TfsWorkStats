using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsUtils
{
	public class ChangesFilter
	{
		public List<ChangeType> ChangeTypes { get; set; }

		public List<string> FileExtensions { get; set; }

		public bool IsFilterPassed(Change change)
		{
			if (ChangeTypes != null
				&& ChangeTypes.Count > 0
				&& ChangeTypes.Any(c => c == change.ChangeType))
				return true;

			string fileExtension = Path.GetExtension(change.Item.ServerItem);
			if (FileExtensions != null
				&& FileExtensions.Count > 0
				&& FileExtensions.Any(f => f == fileExtension))
					return true;

			return false;
		}
	}
}
