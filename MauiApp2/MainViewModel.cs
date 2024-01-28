using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp2;

/// <summary>
///
/// </summary>
[INotifyPropertyChanged]
public partial class MainViewModel
{
    [ObservableProperty] private string _name;

    /// <summary>
    ///
    /// </summary>
    public ObservableCollection<User> Users { get; } = new();


    private IDispatcher _dis;

    public MainViewModel(IDispatcher dispatcher)
    {
        this._dis = dispatcher;
        Users.Add(new() { Name = "john", Age = 30, Day = DateTime.Now });

        Users.Add(new() { Name = "aaa", Age = 30, Day = DateTime.Now.AddDays(-1) });

        Users.Add(new() { Name = "bbb", Age = 30, Day = DateTime.Now.AddDays(-2) });
    }

    public async Task<string> GetHttp()
    {
        Name = "start";
        var msg = string.Empty;
        // Issue the HTTP request and let the thread return from GetHttp
        try
        {
            msg = await new HttpClient().GetStringAsync("http://www.baidu.com");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // throw;
        }

        Name = "done";
// We never get here: The GUI thread is waiting for this method to finish but this method
// can't finish because the GUI thread is waiting for it to finish --> DEADLOCK!
        return msg;
    }
}
