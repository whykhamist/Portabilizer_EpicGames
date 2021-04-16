
namespace Portable
{
	/// <summary>
	/// This is a manifest file with minimal properties used. Information here is the least EpicGames Launcher can use
	/// to build a complete manifest.
	/// </summary>
    public class MiniManifest : Catalog
	{
		public string ManifestLocation
		{
			get;
			set;
		}

		public string BuildLabel
		{
			get;
			set;
		}

		public string InstallationGuid
		{
			get;
			set;
		}

		public string InstallLocation
		{
			get;
			set;
		}

		public string StagingLocation
		{
			get;
			set;
		}

		public string MainGameAppName
		{
			get;
			set;
		}

		public string MandatoryAppFolderName
		{
			get;
			set;
		}
	}
}
