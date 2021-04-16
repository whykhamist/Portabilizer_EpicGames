using Configuration;
using System;

namespace Portable
{
    public class Plugin : IPlugin
    {
        /// <summary>
        /// You can put your configuration here instead.
        /// Note that Config.json file will be appended over this configuration.
        /// Config not set on the config file 
        /// </summary>
        public IConfiguration Config { get { return new Configuration(); } }

        /// <summary>
        /// Your fix class here.
        /// </summary>
        public IFix Fix { get { return new Fix(); } }

        /// <summary>
        /// The registry content here will be applied to the registry before the app applies the content of found PREG files.
        /// You can compile your registry content as string resource for easier access.
        /// </summary>
        public string RegistryContent { get { return string.Empty; } }

        /// <summary>
        /// Your icon here in bytes.
        /// Compile your icon as a resource and put it here.
        /// </summary>
        public byte[] IconBytes { get { return Properties.Resources.favico; } }
    }
}
