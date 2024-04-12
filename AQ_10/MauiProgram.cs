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
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<EndPage>();
        builder.Services.AddTransient<SceneOne>();
        builder.Services.AddTransient<SceneTwo>();
        builder.Services.AddTransient<SceneThree>();
        builder.Services.AddTransient<SceneFour>();
        builder.Services.AddTransient<SceneFive>();
        builder.Services.AddTransient<SceneSix>();
        builder.Services.AddTransient<SceneSeven>();
        builder.Services.AddTransient<SceneEight>();
        builder.Services.AddTransient<SceneNine>();
        builder.Services.AddTransient<SceneTen>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

