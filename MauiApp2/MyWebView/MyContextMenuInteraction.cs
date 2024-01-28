using System.Diagnostics.CodeAnalysis;

using CoreGraphics;

using Foundation;

using Microsoft.Maui.Platform;

using UIKit;

namespace MauiApp2;

/// <summary>
///
/// </summary>
public class MyContextMenuInteraction : UIContextMenuInteraction
{
    WeakReference<IElementHandler> _handler;

    // Store a reference to the platform delegate so that it is not garbage collected
    [UnconditionalSuppressMessage("Memory", "MEM0002", Justification = "Delegate is weak on Objective-C side")]
    readonly IUIContextMenuInteractionDelegate? _uiContextMenuInteractionDelegate;

    /// <summary>
    ///
    /// </summary>
    /// <param name="handler"></param>
    public MyContextMenuInteraction(IElementHandler handler)
        : base(CreateDelegate(out var del))
    {
        _uiContextMenuInteractionDelegate = del;
        _handler = new WeakReference<IElementHandler>(handler);
    }

    static IUIContextMenuInteractionDelegate CreateDelegate(out IUIContextMenuInteractionDelegate del) =>
        del = new FlyoutUIContextMenuInteractionDelegate();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public UIContextMenuConfiguration? GetConfigurationForMenu()
    {
        var contextFlyout = (Handler?.VirtualView as IContextFlyoutElement)?.ContextFlyout;
        var mauiContext = Handler?.MauiContext;

        if (contextFlyout == null || mauiContext == null)
            return null;

        //contextFlyout.Handler?.DisconnectHandler();
        var contextFlyoutHandler = contextFlyout.ToHandler(mauiContext);
        var contextFlyoutPlatformView = contextFlyoutHandler.PlatformView;

        if (contextFlyoutPlatformView is UIMenu uiMenu)
        {
            return UIContextMenuConfiguration.Create(
                identifier: null,
                previewProvider: null,
                actionProvider: _ => uiMenu);
        }

        return null;
    }

    IElementHandler? Handler
    {
        get
        {
            if (_handler.TryGetTarget(out var handler))
            {
                return handler;
            }

            return null;
        }
    }

    sealed class FlyoutUIContextMenuInteractionDelegate : NSObject, IUIContextMenuInteractionDelegate
    {
        public FlyoutUIContextMenuInteractionDelegate()
        {
        }

        public UIContextMenuConfiguration? GetConfigurationForMenu(UIContextMenuInteraction interaction,
            CGPoint location)
        {
            //TODO:may be maui interaction
            if (interaction is MyContextMenuInteraction contextMenu)
                return contextMenu.GetConfigurationForMenu();

            return null;
        }
    }
}
