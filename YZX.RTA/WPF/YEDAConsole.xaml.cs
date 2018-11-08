using System;
using System.Threading;
using System.Windows;

namespace YZX.WINCC.Controls
{
  public partial class YEDAConsole : Window
  {
    #region Trace
    public static bool ConsoleLoaded = false;
    #endregion Trace

    public static void OpenInThread()
    {
      Thread newThread = new Thread(Open);
      newThread.SetApartmentState(ApartmentState.STA);
      newThread.Start();
    }

    public static void Open()
    {
      YEDAConsole c = new YEDAConsole();
      c.Show();
    }

    public YEDAConsole()
    {
      InitializeComponent();
      Loaded += new RoutedEventHandler(YEDAConsole_Loaded);
      Closed += new EventHandler(YEDAConsole_Closed);
    }

    void YEDAConsole_Loaded(object sender, RoutedEventArgs e)
    {
      ConsoleLoaded = true;

      PYC.Loaded += PYC_Loaded;

      //PYC.Console.ScriptScope.SetVariable("SW", sw);

      //PYC.Console.SetDispatcher(Dispatcher);
    }

    private void PYC_Loaded(object sender, RoutedEventArgs e)
    {
      PYC.Console.AllowFullAutocompletion = true;
      PYC.Console.DisableAutocompletionForCallables = false;
    }

    void YEDAConsole_Closed(object sender, EventArgs e)
    {
      ConsoleClosed = true;
      ConsoleLoaded = false;

      PYC.Console.Dispose();
    }

    #region YEDAConsole单例模式
    private static YEDAConsole instance;
    private static bool ConsoleClosed = false;

    public static YEDAConsole Instance
    {
      get
      {
        if (ConsoleClosed == true)
        {
          instance = new YEDAConsole();
        }
        else
        {
          if (instance == null)
          {
            instance = new YEDAConsole();
          }
        }

        return instance;
      }
    }
    #endregion YEDAConsole单例模式
  }
}
