using SimpleSyndicate.Models;
using SimpleSyndicate.Mvc.Controllers.VersionHistory;
using SimpleSyndicate.Repositories;

namespace WebApi_swagger.Controllers.Home.VersionHistory
{
	/// <summary>
	/// Version history controller.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Already shipping library.")]
	public class VersionHistoryController : GenericVersionHistoryController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VersionHistoryController"/> class.
		/// </summary>
		/// <param name="versionHistoryItemRepository"><see cref="VersionHistoryItem"/> repository.</param>
		public VersionHistoryController(IRepository<VersionHistoryItem> versionHistoryItemRepository)
			: base(versionHistoryItemRepository)
		{
		}
	}
}