using CommunityToolkit.Maui;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Handlers;

using WebKit;

namespace MauiApp2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            //.UseMauiCompatibility()
            .UseMauiCommunityToolkit()
            .ConfigureDispatching()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        ViewHandler.ViewMapper.Add(nameof(IContextFlyoutElement.ContextFlyout), MyWebView.MapContextFlyoutDelegate);
        MyWebViewHandler.Mapper.Add(nameof(WKUIDelegate), MyWebView.MapWKUIDelegate);

        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(MyWebView), typeof(MyWebViewHandler));
            handlers.AddHandler(typeof(MenuFlyout), typeof(MenuFlyoutHandler));
        });

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
