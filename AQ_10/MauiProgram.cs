using Microsoft.Extensions.Logging;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

