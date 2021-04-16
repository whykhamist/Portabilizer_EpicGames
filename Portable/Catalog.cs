
namespace Portable
{
	/// <summary>
	/// The Catalog file is found in every EpicGames Game folders.
	/// This file provides information to the EpicGames Launcher that it is one of its games.
	/// </summary>
    public class Catalog
	{
		public int FormatVersion
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string CatalogItemId
		{
			get;
			set;
		}

		public string CatalogNamespace
		{
			get;
			set;
		}
	}
}
