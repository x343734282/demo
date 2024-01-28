using Foundation;

using ObjCRuntime;

using UIKit;

namespace MauiApp2;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
#if MACCATALYST
        Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("Inspect", (handler, view) =>
        {
            if (OperatingSystem.IsMacCatalystVersionAtLeast(16, 4))
                handler.PlatformView.Inspectable = true;
        });
#endif
        return MauiProgram.CreateMauiApp();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    public override void BuildMenu(IUIMenuBuilder builder)
    {
        Console.WriteLine("build menu");

        foreach (var name in Enum.GetValues(typeof(UIMenuIdentifier)))
        {
            if (((UIMenuIdentifier)name) != UIMenuIdentifier.None && ((UIMenuIdentifier)name) != UIMenuIdentifier.Root)
            {
                builder.RemoveMenu(((UIMenuIdentifier)name).GetConstant());
            }
        }

        base.BuildMenu(builder);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="action"></param>
    /// <param name="withSender"></param>
    /// <returns></returns>
    public override bool CanPerform(Selector action, NSObject? withSender)
    {
        Console.WriteLine($"action name is : {action.Name}");
        return false;
        // return base.CanPerform(action, withSender);
    }
}
