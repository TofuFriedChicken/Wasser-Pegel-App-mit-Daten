using Microsoft.Extensions.Logging;
using Pegel_Wetter_DFFUDC.Model;

namespace Pegel_Wetter_DFFUDC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ModelInputintoHistory>();
            builder.Services.AddSingleton<InputRainfallData>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
