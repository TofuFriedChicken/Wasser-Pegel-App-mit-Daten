using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    public static class RainConfiguration
    {
        public static string ExtractPath => Path.Combine(FileSystem.AppDataDirectory, "RainfallOpenDataViewModel");
        public static Stream GetEmbeddedResourceStream(string resourceFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"Pegel_Wetter_DFFUDC.Resources.Raw.{resourceFileName}";

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
