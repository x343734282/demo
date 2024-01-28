using Microsoft.Maui.Handlers;

using PlatformView = UIKit.UIView;

namespace MauiApp2;

/// <summary>
///
/// </summary>
public class MyWebView : WebView
{
    public static void MapWKUIDelegate(IWebViewHandler handler, IWebView webView)
    {
        if (handler is WebViewHandler platformHandler)
            handler.PlatformView.UIDelegate = new WebViewDelegate(handler);
    }

    public static void MapContextFlyoutDelegate(IViewHandler handler, IView view)
    {
        //ViewHandler.MapContextFlyout(handler, view);

        if (view is IContextFlyoutElement contextFlyoutContainer)
        {
            MapContextFlyout(handler, contextFlyoutContainer);
        }
    }

    private static void MapContextFlyout(IElementHandler handler, IContextFlyoutElement contextFlyoutContainer)
    {
        _ = handler.MauiContext ??
            throw new InvalidOperationException($"The handler's {nameof(handler.MauiContext)} cannot be null.");

        if (handler.PlatformView is PlatformView uiView)
        {
            MyContextMenuInteraction? currentInteraction = null;

            foreach (var interaction in uiView.Interactions)
            {
                if (interaction is MyContextMenuInteraction menuInteraction)
                    currentInteraction = menuInteraction;
            }

            if (contextFlyoutContainer.ContextFlyout != null)
            {
                if (currentInteraction == null)
                    uiView.AddInteraction(new MyContextMenuInteraction(handler));
            }
            else if (currentInteraction != null)
            {
                uiView.RemoveInteraction(currentInteraction);
            }
        }
    }
}
