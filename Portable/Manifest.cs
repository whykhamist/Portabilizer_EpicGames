
namespace Portable
{
	/// <summary>
	/// The Manifest file Structure.
	/// The manifest file is found in your ProgramData folder. A manifest file has more detailed information about 
	/// a game. This is required for your game to show up and run in EpicGames Launcher.
	/// </summary>
    public class Manifest : MiniManifest
	{
		public string DisplayName
		{
			get;
			set;
		}

		public string FullAppName
		{
			get;
			set;
		}

		public bool bIsIncompleteInstall
		{
			get;
			set;
		}

		public string AppVersionString
		{
			get;
			set;
		}

		public string LaunchCommand
		{
			get;
			set;
		}

		public string LaunchExecutable
		{
			get;
			set;
		}

		public bool bIsApplication
		{
			get;
			set;
		}

		public bool bIsExecutable
		{
			get;
			set;
		}

		public bool bIsManaged
		{
			get;
			set;
		}

		public bool bNeedsValidation
		{
			get;
			set;
		}

		public bool bRequiresAuth
		{
			get;
			set;
		}

		public bool bCanRunOffline
		{
			get;
			set;
		}

		public string[] BaseURLs
		{
			get;
			set;
		}

		public string[] AppCategories
		{
			get;
			set;
		}

		public string[] ChunkDbs
		{
			get;
			set;
		}

		public string[] CompatibleApps
		{
			get;
			set;
		}

		public string InstallSessionId
		{
			get;
			set;
		}

		public string[] InstallTags
		{
			get;
			set;
		}

		public string[] InstallComponents
		{
			get;
			set;
		}

		public string HostInstallationGuid
		{
			get;
			set;
		}

		public string[] PrereqIds
		{
			get;
			set;
		}

		public string TechnicalType
		{
			get;
			set;
		}

		public string VaultThumbnailUrl
		{
			get;
			set;
		}

		public string VaultTitleText
		{
			get;
			set;
		}

		public long InstallSize
		{
			get;
			set;
		}

		public string MainWindowProcessName
		{
			get;
			set;
		}

		public string[] ProcessNames
		{
			get;
			set;
		}

		public string OwnershipToken
		{
			get;
			set;
		}
	}
}
