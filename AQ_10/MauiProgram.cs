using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace AQ_10;

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
				fonts.AddFont("Montserrat-ExtraBold.ttf", "MontserratExtraBold");
                fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");

            });

        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddTransient<SceneOne>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

