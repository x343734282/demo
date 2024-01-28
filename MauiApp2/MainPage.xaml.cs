using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Maui.Controls;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    int count = 0;

    /// <summary>
    ///
    /// </summary>
    public ObservableCollection<User> Users { set; get; }

    /// <summary>
    ///
    /// </summary>
    public string Name { get; set; }

    private readonly MainViewModel _viewModel;

    /// <summary>
    ///
    /// </summary>
    public MainPage(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        Users = new ObservableCollection<User>
        {
            new() { Name = "john", Age = 30 }, new() { Name = "aaa", Age = 30 }, new() { Name = "bbb", Age = 30 }
        };

        BindingContext = _viewModel;

        InitializeComponent();

        try
        {
            // var item1 = new MenuFlyoutItem { Text = "test1" };
            // var item2 = new MenuFlyoutItem { Text = "test2" };
            // var menuFlyout = new MenuFlyout { item1, item2 };
            // FlyoutBase.SetContextFlyout(MyWebView, menuFlyout);
            //
            // TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            // tapGestureRecognizer.Buttons = ButtonsMask.Secondary;
            // tapGestureRecognizer.Tapped += delegate
            // {
            //     Console.WriteLine("click trigger!");
            // };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // throw;
        }
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        Console.WriteLine(Environment.CurrentManagedThreadId);

        // _viewModel.GetHttp();
        await GetHttp();

        var fileName = "text";
        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents",
            fileName);

        try
        {
            using (var fileStream = File.OpenWrite("/Users/johnxie/Documents/text"))
            {
                // fileStream.
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }


        //users.Add(new User());

        // SemanticScreenReader.Announce(CounterBtn.Text);
    }

    public async Task<string> GetHttp()
    {
        var msg = string.Empty;
        // Issue the HTTP request and let the thread return from GetHttp
        try
        {
            msg = await new HttpClient().GetStringAsync("https://www.baidu.com").ConfigureAwait(false);
            Dispatcher.Dispatch(() =>
            {
                //Status.Text = "ok";

                Users.Add(new User { Name = "7384", Age = 20 });
                // Users.RemoveAt(0);
                // Users.Move(1, 2);
                // Users.Append(new User() { Name = "000", Age = 20 });
                // Users.Prepend(new User() { Name = "eee", Age = 20 });
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // throw;
        }

// We never get here: The GUI thread is waiting for this method to finish but this method
// can't finish because the GUI thread is waiting for it to finish --> DEADLOCK!
        return msg;
    }
}

/// <summary>
///
/// </summary>
public class User
{
    /// <summary>
    ///
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime Day { get; set; }
}
