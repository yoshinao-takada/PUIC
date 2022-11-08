using static PInvoke.User32;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using System.Runtime.InteropServices.ObjectiveC;
using Microsoft.UI.Xaml;
using System;

namespace PUIC
{
    static internal class ControlUtils
    {

        static internal AppWindow GetAncestorWindow(object uiElement, GetAncestorFlags flags)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(uiElement);
            var hwndAncestor = GetAncestor(hwnd, flags);
            var windowID = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwndAncestor);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowID);
            return appWindow;
        }
        static internal SizeInt32 GetAncestorWindowSize(object uiElement, GetAncestorFlags flags)
        {
            return GetAncestorWindow(uiElement, flags).Size;
        }

        static bool HasISharedGeometryConsumer(DependencyObject any)
        {
            bool result = true;
            ISharedGeometryConsumer theInterface;
            Console.WriteLine(any.GetType().ToString());
            try
            {
                theInterface = (ISharedGeometryConsumer)any;
            }
            catch (System.Exception)
            {
                result = false;
            }
            return result;
        }

        static internal ISharedGeometryConsumer GetISharedGeometryConsumer(DependencyObject any)
        {
            if (HasISharedGeometryConsumer(any))
            {
                return any as ISharedGeometryConsumer;
            }
            else
            {
                return GetISharedGeometryConsumer(((FrameworkElement)any).Parent);
            }
        }
    }
}
