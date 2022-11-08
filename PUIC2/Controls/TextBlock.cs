using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI;

namespace PUIC2.Controls
{
    public interface ITextBlock
    {
        /// <summary>
        /// Accessor to the base text block originally supplied by WinUI3.
        /// </summary>
        Microsoft.UI.Xaml.Controls.TextBlock CoreObject { get; set; }

        /// <summary>
        /// Accessor to the internal text
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// (font size)/(object height)
        /// </summary>
        double FontSizePerHeight { set; }
    }
    public class TextBlock : Grid, IUiControl, IBordered, ITextBlock
    {
        Color[] _colors;
        Thickness _edgeMask;
        CornerRadius _cornerMask;
        Microsoft.UI.Xaml.Controls.TextBlock _core;
        double _fontSizePerHeight;
        ISharedGeometryConsumer _sharedGeometry;
        #region Constructor and its related methods
        public TextBlock() : base()
        {
            _edgeMask = new Thickness(0);
            _cornerMask = new CornerRadius(0);
            _core = new Microsoft.UI.Xaml.Controls.TextBlock();
            _fontSizePerHeight = 0.8;
            SizeChanged += OnSizeChanged;
            Loaded += OnLoaded;
        }

        SizeChangedEventArgs _e;
        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _e = e;
            if (IsLoaded)
            {
                _core.FontSize = _fontSizePerHeight * ActualHeight;
                UpdateBorder();
            }
        }

        void OnLoaded(object sener, RoutedEventArgs e)
        {
            _core.FontSize = _fontSizePerHeight * ActualHeight;
            UpdateBorder();
        }
        #endregion
        #region IUiControl impl
        public Type DerivedFrom => typeof(Grid);
        public void Attach(IPUIC ancestor)
        {
            if (ancestor.DerivedFrom == typeof(Grid))
            {
                (ancestor as Grid).Children.Add(this);
            }
            else if (ancestor.DerivedFrom == typeof(Border))
            {
                throw new NotImplementedException();
                //(ancestor as Border).Child = this;
            }
            else if (ancestor.DerivedFrom == typeof(Window))
            {
                (ancestor as Window).Content = this;
            }
        }

        public void Detach()
        {
            throw new NotImplementedException();
        }
        public ISharedGeometryConsumer SharedGeometry
        {
            get => _sharedGeometry;
            set
            {
                _sharedGeometry = value;
                UpdateBorder();
            }
        }

        void UpdateBorder()
        {
            double borderW = _sharedGeometry.SharedBorderThickenss;
            BorderThickness = new Thickness(
                EdgeMask.Left * borderW, EdgeMask.Top * borderW,
                EdgeMask.Right * borderW, EdgeMask.Bottom * borderW);
            double cornerR = _sharedGeometry.SharedCornerRadius;
            CornerRadius = new CornerRadius(
                CornerMask.TopLeft * cornerR, CornerMask.TopRight * cornerR,
                CornerMask.BottomRight * cornerR, CornerMask.BottomLeft);
        }
        #endregion

        #region IBordered impl
        public Color[] Colors { set => _colors = value; }
        public Thickness EdgeMask { get => _edgeMask; set => _edgeMask = value; }

        public CornerRadius CornerMask { get => _cornerMask; set => _cornerMask = value; }
        public bool AllEdges { set => _edgeMask = new Thickness(0); }
        public bool AllCorners { set => _cornerMask = new CornerRadius(0); }
        #endregion

        #region ITextBlock impl
        public string Text
        {
            get => _core.Text;
            set => _core.Text = value;
        }
        public double FontSizePerHeight 
        {
            set
            {
                _fontSizePerHeight = value;
                if (IsLoaded)
                {
                    CoreObject.FontSize = _fontSizePerHeight * ActualHeight;
                }
            }
        }
        public Microsoft.UI.Xaml.Controls.TextBlock CoreObject
        { get => _core; set => _core = value; }
        #endregion
    }
}
