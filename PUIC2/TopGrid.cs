using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using System;
using Windows.Graphics;
using System.ComponentModel;

namespace PUIC2
{
    public class TopGrid : Grid, ITopContainer, ISharedGeometryProvider, ISharedGeometryConsumer
    {
        protected Window _window;
        protected AppWindow _appWindow;
        protected IntPtr _hwnd;
        protected WindowId _id;
        protected bool _isAspectRatioFixed;
        protected SizeInt32 _fixedWidthAndHeightRatio;
        protected (int, int) _borderThicknessPerClientLength, _cornerRadiusPerClientLength;
        protected ProportionalTo _proportionalTo;

        public Type DerivedFrom { get => typeof(Grid); }

        #region ITopContainer impl
        public void Attach(Window window)
        {
            _window = window;
            _hwnd = WinRT.Interop.WindowNative.GetWindowHandle(_window);
            _id = Win32Interop.GetWindowIdFromWindow(_hwnd);
            _appWindow = AppWindow.GetFromWindowId(_id);
            _window.Content = this;
        }

        public AppWindow AppWindow => _appWindow;
        #endregion ITopContainer impl

        #region ISharedGeometryProvider impl
        public int ClientWidth
        {
            get => _appWindow.ClientSize.Width;
            set
            {
                var size = _appWindow.ClientSize;
                size.Width = value;
                if (_isAspectRatioFixed)
                {
                    size.Height = (int)
                        (value * _fixedWidthAndHeightRatio.Height / (double)_fixedWidthAndHeightRatio.Width);
                }
                _appWindow.ResizeClient(size);
                Changed?.Invoke(this, new PropertyChangedEventArgs(nameof(SharedWindowWidth)));
            }
        }
        public int ClientHeight
        {
            get => _appWindow.ClientSize.Height;
            set
            {
                var size = _appWindow.ClientSize;
                size.Height = value;
                if (_isAspectRatioFixed)
                {
                    size.Width = (int)
                        (value * _fixedWidthAndHeightRatio.Width / (double)_fixedWidthAndHeightRatio.Height);
                }
                _appWindow.ResizeClient(size);
                Changed?.Invoke(this, new PropertyChangedEventArgs(nameof(SharedWindowHeight)));
            }
        }
        public bool IsAspectRatioFixed 
        {
            get => _isAspectRatioFixed;
            set
            {
                _isAspectRatioFixed = value;
                if (_isAspectRatioFixed)
                {
                    _fixedWidthAndHeightRatio = _appWindow.ClientSize;
                }
            }
        }
        public double BorderThicknessPerClientLengthD
        {
            get => _borderThicknessPerClientLength.Item1 / (double)_borderThicknessPerClientLength.Item2;
        }
        public double CornerRadiusPerClientLengthD 
        {
            get => _cornerRadiusPerClientLength.Item1 / (double)_cornerRadiusPerClientLength.Item2;
        }
        public (int, int) BorderThicknessPerClientLength
        {
            get => _borderThicknessPerClientLength;
            set => _borderThicknessPerClientLength = value;
        }
        public (int, int) CornerRadiusPerClientLength
        {
            get => _cornerRadiusPerClientLength;
            set => _cornerRadiusPerClientLength = value;
        }

        public ProportionalTo ProportionalTo { get => _proportionalTo; set => _proportionalTo = value; }
        #endregion ISharedGeometryProvider impl
        #region ISharedGeometryConsumer impl
        public event PropertyChangedEventHandler Changed;

        public double SharedWindowWidth => _appWindow.ClientSize.Width - (BorderThickness.Left + BorderThickness.Right);

        public double SharedWindowHeight => _appWindow.ClientSize.Height - (BorderThickness.Top + BorderThickness.Bottom);

        public double SharedBorderThickenss => _borderThicknessPerClientLength.Item1 * 
            (_proportionalTo == ProportionalTo.Width ? SharedWindowWidth : SharedWindowHeight) /
            (double)_borderThicknessPerClientLength.Item2;

        public double SharedCornerRadius => _cornerRadiusPerClientLength.Item1 *
            (_proportionalTo == ProportionalTo.Width ? SharedWindowWidth : SharedWindowHeight) /
            (double)_cornerRadiusPerClientLength.Item2;

        public ISharedGeometryConsumer SharedGeometry => this;
        #endregion ISharedGeometryConsumer impl
    }
}
