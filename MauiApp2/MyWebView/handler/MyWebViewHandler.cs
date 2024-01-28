using Microsoft.Maui.Handlers;

using WebKit;

namespace MauiApp2;

/// <summary>
///
/// </summary>
public partial class MyWebViewHandler : WebViewHandler
{
    protected override void ConnectHandler(WKWebView platformView)
    {
// platformView
        base.ConnectHandler(platformView);
    }

    protected override void SetupContainer()
    {
        base.SetupContainer();
    }

    // protected override WKWebView CreatePlatformView()
    // {
    //     return base.CreatePlatformView();
    // }

    /// <summary>
    ///
    /// </summary>
    /// <param name="platformView"></param>
    protected override void DisconnectHandler(WKWebView platformView)
    {
        platformView.Dispose();
        base.DisconnectHandler(platformView);
    }
}
