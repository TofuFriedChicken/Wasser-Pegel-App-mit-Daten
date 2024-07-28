using Microsoft.Extensions.Logging;
using Pegel_Wetter_DFFUDC.Model;
using CommunityToolkit.Maui.Maps;
using Microsoft.Maui.Maps;
using SkiaSharp.Views.Maui.Controls.Hosting;
using CommunityToolkit.Maui;

namespace Pegel_Wetter_DFFUDC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMaps("beoVW2DRXJmNKDNXAU3a~q9N4iCClq08h7W7Z_LEFYA~AgM5IrnYZ6nogeA56gnZWzZqEOiCQWUEFGmWHr34e8fZhygfdT8EAEo44VQptTmE")

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                });

            builder.Services.AddSingleton<ModelInputintoHistory>();



          //  builder.Services.AddTransient<swapDates>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
    }
}
