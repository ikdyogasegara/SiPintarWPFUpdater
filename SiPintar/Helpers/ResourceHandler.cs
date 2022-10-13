using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Resources.NetStandard;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ResourceHandler
    {
        public const string resxFile = @".\Resources.resx";
        private readonly Dictionary<string, string> resourceDictionaries;

        public ResourceHandler()
        {
            var resxReader = new ResXResourceReader(resxFile);
            resourceDictionaries = resxReader.Cast<DictionaryEntry>()
                                    .ToDictionary(r => r.Key.ToString(), r => r.Value.ToString());
        }

        public Dictionary<string, string> getResourceDictionary()
        {
            return resourceDictionaries;
        }

        public static string GetResourceValue(string key)
        {
            try
            {
                var result = App.resources.SingleOrDefault(i => i.Key == key);

                return !result.Equals(new KeyValuePair<string, string>()) ? (result.Value ?? "Empty Response Message") : "Response Message not found";
            }
            catch (Exception)
            {
                return "Response Message not found";
            }
        }
    }
}
