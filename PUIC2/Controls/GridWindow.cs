using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;

namespace PUIC2.Controls
{
    public class GridWindow : WindowBase, ITopContainer, ISharedGeometryProvider
    {
        protected TopGrid _topGrid;
        public GridWindow() : base()
        {
            _topGrid = new TopGrid();
            _topGrid.Attach(this);
        }
        #region ITopContainer impl
        public void Attach(Window window) {  throw new NotImplementedException(); }

        public AppWindow AppWindow => _topGrid.AppWindow;

        public ISharedGeometryConsumer SharedGeometry => _topGrid;
        #endregion ITopContainer impl

        #region ISharedGeoemtryProvider impl
        public int ClientWidth { get => _topGrid.ClientWidth; set => _topGrid.ClientWidth = value; }

        public int ClientHeight { get => _topGrid.ClientHeight; set => _topGrid.ClientHeight = value; }

        public bool _isAspectRatioFixed;
        public bool IsAspectRatioFixed
        {
            get => _topGrid.IsAspectRatioFixed;
            set => _topGrid.IsAspectRatioFixed = value;
        }

        public double BorderThicknessPerClientLengthD
        { get => _topGrid.BorderThicknessPerClientLengthD; }

        public double CornerRadiusPerClientLengthD
        { get => _topGrid.CornerRadiusPerClientLengthD; }

        public (int, int) BorderThicknessPerClientLength
        { get => _topGrid.BorderThicknessPerClientLength; set => _topGrid.BorderThicknessPerClientLength = value; }

        public (int, int) CornerRadiusPerClientLength
        { get => _topGrid.CornerRadiusPerClientLength; set => _topGrid.CornerRadiusPerClientLength = value; }

        public ProportionalTo ProportionalTo { get => _topGrid.ProportionalTo; set => _topGrid.ProportionalTo = value; }
        #endregion ISharedGeoemtryProvider impl

        public TopGrid TopGrid { get => _topGrid; }
    }
}
