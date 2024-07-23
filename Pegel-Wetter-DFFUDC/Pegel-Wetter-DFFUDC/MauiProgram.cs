using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Maps;
using Microsoft.Maui.Maps;

namespace Pegel_Wetter_DFFUDC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()

                //API key for bingmaps
                .UseMauiCommunityToolkitMaps("1L5DIEOsrWUGhOGrcRQH~K3FfsbEonXTrbGtgx6vWrw~AlMAjdmTc86UMHN0fdFlxsxoUFZhIvgMUnISDN8K0db3kaymimRqUZ7feYTHn_y3")

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
