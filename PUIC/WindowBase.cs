using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using Windows.Graphics;
using System.ComponentModel;
using Windows.UI.Core;

namespace PUIC
{

    public class WindowBase : Window, ISharedGeometryConsumer, ISharedGeometryProvider
        , IWindowPositioner, IPUICObject
    {

        public event EventHandler<PropertyChangedEventArgs> SharedGeometryChanged;

        double _borderThicknessPerHeight, _cornerRadiusPerHeight, _widthPerHeight;
        bool IsWidthPerHeightControlled { get; set; }
        
        AppWindow _myAppWindow;
        public double BorderThicknessPerHeight { set => _borderThicknessPerHeight = value; }
        public double CornerRadiusPerHeight { set => _cornerRadiusPerHeight = value; }

        public double SharedBorderThickness => _borderThicknessPerHeight * Bounds.Height;

        public double SharedCornerRadius => _cornerRadiusPerHeight * Bounds.Height;

        public WindowBase Covering
        {
            get
            {
                WindowBase newWindow = new WindowBase();
                newWindow._myAppWindow.Move(_myAppWindow.Position);
                return newWindow;
            }
        }

        public double WidthPerHeight
        {
            set
            {
                _widthPerHeight = value;
                IsWidthPerHeightControlled = (_widthPerHeight > 0);
                if (IsWidthPerHeightControlled)
                {
                    SizeInt32 newSize = new SizeInt32()
                    {
                        Height = (int)Bounds.Height,
                        Width = (int)(Bounds.Height * _widthPerHeight),
                    };
                    _myAppWindow.Resize(newSize);
                }
            }
        }

        public ISharedGeometryConsumer SharedGeometry
        {
            get => this;
            set => throw new NotImplementedException(); 
        }
        public IPUICObject PUICParent 
        {
            get => null;
            set => throw new NotImplementedException(); 
        }

        static bool AreEqual(double d0, double d1, double rTol)
        {
            double relDiff = Math.Abs(d0 - d1) / Math.Abs(d0 + d1);
            return relDiff < rTol;
        }
        public bool AlignToScreen(double wPerH, double rTol)
        {
            DisplayArea found = null;
            var dispAreas = DisplayArea.FindAll();
            foreach (var area in dispAreas)
            {
                double areaWPerH = (double)area.OuterBounds.Width / (double)area.OuterBounds.Height;
                if (AreEqual(areaWPerH, wPerH, rTol))
                {
                    found = area; break;
                }
            }
            if (found == null)
            {
                return false;
            }

            var moveTo = new Windows.Graphics.PointInt32(found.OuterBounds.X, found.OuterBounds.Y);
            _myAppWindow.Move(moveTo);
            return true;
        }

        public WindowBase() : base()
        {
            IsWidthPerHeightControlled = false;
            _borderThicknessPerHeight = 2.0 / 768;
            _cornerRadiusPerHeight = 5.0 / 768;
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            _myAppWindow = AppWindow.GetFromWindowId(id);

            // register WindowID and this window object itself to the application wide dictionary of
            // top level windows.
            SharedWindows.TheInstance.Add(id, this);
            Closed += OnClose;
            SizeChanged += OnSizeChanged;
        }

        protected virtual void OnClose(object sender, WindowEventArgs e)
        {
            // remove the item from the dictionary of top level windows.
            SharedWindows.TheInstance.Remove(_myAppWindow.Id);
        }
        protected virtual void OnSizeChanged(object sender, Microsoft.UI.Xaml.WindowSizeChangedEventArgs e)
        {
            // control aspect ratio if it is restricted.
            if (IsWidthPerHeightControlled)
            {
                SizeInt32 newSize = new SizeInt32()
                {
                    Height = (int)Bounds.Height,
                    Width = (int)(_widthPerHeight * Bounds.Height)
                };
                _myAppWindow.Resize(newSize);
            }

            // notify PUIC children of window size change.
            SharedGeometryChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
