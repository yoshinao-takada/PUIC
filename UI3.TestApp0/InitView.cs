using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.UI;
using PUIC2;
using PUIC2.Controls;
using Microsoft.UI;

namespace UI3.TestApp0
{
    internal class InitView
    {
        static InitView()
        {

        }

        internal void Init(GridWindow window)
        {
            InitGrid(window.TopGrid);
            InitSharedGeometry(window);
            AddTextBlock(window.TopGrid, "Hello WinUI3\nTestApp0 : Ver.1.0");
        }

        void AddTextBlock(TopGrid grid, string caption)
        {
            var initialColors = new Color[] { Colors.Red, Colors.AliceBlue, Colors.Blue, };
            var tb = new PUIC2.Controls.TextBlock()
            {
                Text = caption,
                Colors = initialColors,
            };
            Grid.SetRow(tb.CoreObject, 1);
            Grid.SetColumn(tb.CoreObject, 1);
            
        }

        static readonly double[] widths = new double[] { 20.0, 600.0, 20.0 };
        static readonly double[] heights = new double[] { 20.0, 440.0, 20.0 };
        void InitGrid(Grid grid)
        {
            foreach (var height in heights)
            {
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(height, GridUnitType.Star)
                });
            }
            foreach (var width in widths)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(width, GridUnitType.Star)
                });
            }
        }
        void InitSharedGeometry(ISharedGeometryProvider sharedGeometry)
        {
            sharedGeometry.ClientHeight = 480;
            sharedGeometry.ClientWidth = 640;
            sharedGeometry.IsAspectRatioFixed = true;
            sharedGeometry.ProportionalTo = ProportionalTo.Height;
            sharedGeometry.BorderThicknessPerClientLength = (3, sharedGeometry.ClientHeight);
            sharedGeometry.CornerRadiusPerClientLength = (7, sharedGeometry.ClientHeight);
        }
    }
}
