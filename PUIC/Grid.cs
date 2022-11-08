using System;
using System.ComponentModel;
using Microsoft.UI.Xaml;

namespace PUIC
{
    public interface IGrid
    {
        /// <summary>
        /// Enable/disable each edge selectively. 1.0: enabled, 0.0: disabled
        /// </summary>
        Thickness BorderMask { set; }

        /// <summary>
        /// Enable/disable each corner-rounding selectively. 1.0: enabled, 0.0: disabled
        /// </summary>
        CornerRadius CornerMask { set; }

        /// <summary>
        /// Calculate a margin to contained UI object
        /// </summary>
        /// <param name="relativeMargin">Relative margin represented to a frame rectangle</param>
        /// <param name="row">row selector of a frame</param>
        /// <param name="column">column selector of a frame</param>
        /// <returns></returns>
        Thickness RealMargin(Thickness relativeMargin, int row, int column);

        /// <summary>
        /// Calculate width of a contained UI object
        /// </summary>
        /// <param name="relativeWidth">Relative width represented to a frame column</param>
        /// <param name="column">column selector of a frame</param>
        /// <returns>width represented in pixels</returns>
        double RealWidth(double relativeWidth, int column);

        /// <summary>
        /// Calculate height of a contained UI object
        /// </summary>
        /// <param name="relativeHeight">Relative height represented to a frame row</param>
        /// <param name="row">row selector of a frame</param>
        /// <returns>height represented in pixels</returns>
        double RealHeight(double relativeHeight, int row);
    }

    /// <summary>
    /// Alternative of 
    /// </summary>
    public class Grid : Microsoft.UI.Xaml.Controls.Grid, IGrid, IPUICObject
    {
        double DoubleNaN;

        public Grid() : base()
        {
            DoubleNaN = Height;
            SizeChanged += OnSizeChanged;
            Loaded += OnLoaded;
        }

        double SafeHeight => Double.IsNaN(Height) ? 1.0 : Height;
        double SafeActualHeight => IsLoaded ? ActualHeight : SafeHeight;

        double SafeWidth => Double.IsNaN(Height) ? 1.0 : Width;
        double SafeActualWidth => IsLoaded ? ActualWidth : SafeWidth;

        #region IPUICObject impl
        ISharedGeometryConsumer _sharedGeometry;
        IPUICObject _parent;
        public ISharedGeometryConsumer SharedGeometry 
        {
            get => _sharedGeometry;
            set
            {
                _sharedGeometry = value;
                _sharedGeometry.SharedGeometryChanged += OnSharedGeometryChanged;
            }
        }
        public IPUICObject PUICParent
        {
            get => _parent;
            set => _parent = value; 
        }
        #region ISharedGeometyConsumer imple
        public double SharedBorderThickness => _sharedGeometry.SharedBorderThickness;
        public double SharedCornerRadius => _sharedGeometry.SharedCornerRadius;
        public event EventHandler<PropertyChangedEventArgs> SharedGeometryChanged
        {
            add
            {
                _sharedGeometry.SharedGeometryChanged += value;
            }
            remove
            {
                _sharedGeometry.SharedGeometryChanged -= value;
            }
        }

        protected virtual void OnSharedGeometryChanged(object sender, PropertyChangedEventArgs e)
        {
            double bt = _sharedGeometry.SharedBorderThickness;
            BorderThickness = new Thickness(
                _borderMask.Left * bt, _borderMask.Top * bt,
                _borderMask.Right * bt, _borderMask.Bottom * bt);
            double cr = _sharedGeometry.SharedCornerRadius;
            CornerRadius = new CornerRadius(
                _cornerMask.TopLeft * cr, _cornerMask.TopRight * cr,
                _cornerMask.BottomRight * cr, _cornerMask.BottomLeft * cr);
        }
        #endregion ISharedGeometyConsumer imple
        #endregion IPUICObject impl

        protected virtual void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        #region border and corner radius related
        Thickness _borderMask;
        CornerRadius _cornerMask;
        public Thickness BorderMask { set => _borderMask = value; }
        public CornerRadius CornerMask { set => _cornerMask = value; }
        public double RealHeight(double relativeHeight, int row)
        {
            if (RowDefinitions.Count < 2 && row == 0)
            {
                return SafeActualHeight;
            }
            else if (row < RowDefinitions.Count)
            {
                return RowDefinitions[row].ActualHeight;
            }
            else
            {
                return -1.0;
            }
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_parent != null)
            {
                SharedGeometry = _parent.SharedGeometry;
            }

        }

        public double RealWidth(double relativeWidth, int column)
        {
            if (ColumnDefinitions.Count < 2 && column == 0)
            {
                return SafeActualWidth;
            }
            else if (column < ColumnDefinitions.Count)
            {
                return ColumnDefinitions[column].ActualWidth;
            }
            else
            {
                return -1.0;
            }
        }

        public Thickness RealMargin(Thickness relativeMargin, int row, int column)
        {
            double wFrame = RealWidth(1.0, column);
            double hFrame = RealHeight(1.0, row);
            var realThickness = new Thickness(
                wFrame * relativeMargin.Left, hFrame * relativeMargin.Top,
                wFrame * relativeMargin.Right, hFrame * relativeMargin.Bottom);
            return realThickness;
        }
        #endregion border and corner radius related
    }
}
