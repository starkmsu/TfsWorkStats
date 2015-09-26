using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsUtils
{
	public class ChangesetFilter
	{
		public List<string> WorkItemTypes { get; set; }

		public bool IsFilterPassed(Changeset changeset)
		{
			if (WorkItemTypes != null 
				&& WorkItemTypes.Count > 0
				&& changeset.WorkItems.Any(wi => WorkItemTypes.Any(t => t == wi.Type.Name)))
					return true;

			return false;
		}
	}
}
