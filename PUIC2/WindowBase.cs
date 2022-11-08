using System.Collections.Generic;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;

namespace PUIC2
{
    public class WindowBase : Window, IPUIC, IWindowPositioner
    {
        static bool Match(double x0, double x1, double rTol)
        {
            double relDiff = Math.Abs(x0 - x1) / Math.Abs(x0 + x1);
            return relDiff < rTol;
        }

        AppWindow _appWindow;
        public Type DerivedFrom => typeof(Window);

        public bool AlignToScreen(double wPerH, double rTol, int index)
        {
            var areas = DisplayArea.FindAll();
            List<DisplayArea> matchedAreas = new List<DisplayArea>();
            for (int i = 0; i < areas.Count; i++) // note: foreach is not applicable to IReadOnlyList<T>.
            {
                var area = areas[i];
                double AreaWPerH = (double)area.WorkArea.Width / (double)area.WorkArea.Height;
                if (Match(wPerH, AreaWPerH, rTol))
                {
                    matchedAreas.Add(area);
                }
            }
            if (index >= matchedAreas.Count)
            { // matched area was not found.
                return false;
            }
            var matchedArea = matchedAreas[index];
            PointInt32 targetTopLeft = new PointInt32(matchedArea.WorkArea.X, matchedArea.WorkArea.Y);
            _appWindow.Move(targetTopLeft);
            // The titlebar vanishes in full-screen mode.
            _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            return true;
        }

        public void Resize(double w, double h)
        {
            SizeInt32 newSize = new SizeInt32()
            {
                Width = (int)w,
                Height = (int)h,
            };
            _appWindow.Resize(newSize);
        }

        public WindowBase() : base()
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            _appWindow = AppWindow.GetFromWindowId(id);
        }
    }
}
