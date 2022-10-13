using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class AppVersion
    {
        public static string Version
        {
            get
            {
                //string Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var idx = version.LastIndexOf(".");
                return version.Substring(0, idx);
            }
        }
    }
}
