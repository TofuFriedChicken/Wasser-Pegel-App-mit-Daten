using CommunityToolkit.Maui.Maps;
using Microsoft.Extensions.Logging;

namespace Pegel_Wetter_DFFUDC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMaps("beoVW2DRXJmNKDNXAU3a~q9N4iCClq08h7W7Z_LEFYA~AgM5IrnYZ6nogeA56gnZWzZqEOiCQWUEFGmWHr34e8fZhygfdT8EAEo44VQptTmE")
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
