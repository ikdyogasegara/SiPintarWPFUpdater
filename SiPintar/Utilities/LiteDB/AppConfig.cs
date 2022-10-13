using System.Diagnostics.CodeAnalysis;
using LiteDB;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public class AppConfig
    {
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public ObjectId _id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
