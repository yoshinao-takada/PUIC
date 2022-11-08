using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using PUIC;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PUIC.TestApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new WindowTest();
            ExamineWindow(m_window);
            m_window.Activate();
        }

        public static void ExamineWindow(Window w)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(w);
            var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(id);
            Console.WriteLine($"appWindow.Size = [{appWindow.Size.Width}, {appWindow.Size.Height}]");
        }
        private WindowBase m_window;
    }
}
