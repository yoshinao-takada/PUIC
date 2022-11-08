using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace PUIC.TestApp
{
    internal class WindowTest : WindowBase
    {
        GridTest _grid;
        public WindowTest() : base()
        {
            Content = _grid = new GridTest()
            {
                PUICParent = this,
                SharedGeometry = this,
            };
            
        }
    }

    internal class GridTest : PUIC.Grid
    {
        PUIC.BorderedTextBlock _borderedTextBlock;
        internal GridTest() : base()
        {
            BorderMask = new Microsoft.UI.Xaml.Thickness(1);
            CornerMask = new Microsoft.UI.Xaml.CornerRadius(0,1,0,1);
            BorderBrush = new SolidColorBrush(Colors.Blue);
            Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            _borderedTextBlock = new BorderedTextBlock()
            {
                Text = "日本・Japan\n米国・USA",
                FontSizePerHeight = 0.1,
                TextAlignment = Microsoft.UI.Xaml.TextAlignment.Center,
                SidePaddingPerHeight = 0.5,
                BorderMask = new Microsoft.UI.Xaml.Thickness(1),
                CornerMask = new Microsoft.UI.Xaml.CornerRadius(1),
                BorderBrush = new SolidColorBrush(Colors.Red),
                Background = new SolidColorBrush(Colors.CornflowerBlue),
                PUICParent = this,
            };
            Grid.SetRow(_borderedTextBlock, 1);
            Grid.SetColumn(_borderedTextBlock, 1);
            Children.Add(_borderedTextBlock);
        }

    }
}
