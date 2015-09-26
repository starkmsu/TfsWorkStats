using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsUtils.Accessors
{
	public class ChangesetAccessor
	{
		private readonly VersionControlServer m_versionControlServer;

		public ChangesetAccessor(TfsAccessor accessor)
		{
			m_versionControlServer = accessor.GetVersionControlServer();
		}

		public Changeset GetChangesetById(int changesetId)
		{
			return m_versionControlServer.GetChangeset(changesetId, true, false, true);
		}

		public IEnumerable<Changeset> GetChanges(
			string serverPath,
			ChangesetFilter filter,
			Action<int> progressReportHandler)
		{
			IEnumerable csList = m_versionControlServer.QueryHistory(
				serverPath,
				VersionSpec.Latest,
				0,
				RecursionType.Full,
				null, //any user
				null, // from first changeset
				null, // to last changeset
				int.MaxValue,
				true, // with changes
				false,
				false,
				true); // sorted

			var enumerator = csList.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var changeset = (Changeset)enumerator.Current;

				if (!filter.IsFilterPassed(changeset))
					continue;

				yield return changeset;
			}
		}
	}
}
