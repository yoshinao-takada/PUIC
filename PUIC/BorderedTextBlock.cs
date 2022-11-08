using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace PUIC
{
    public interface IBorderedTextBlock
    {
        string Text { get; set; }

        double FontSizePerHeight { set; }

        double SidePaddingPerHeight { set; }

        Microsoft.UI.Xaml.TextAlignment TextAlignment { set; }
    }
    public class BorderedTextBlock : PUIC.Grid, IMouseOperations, IBorderedTextBlock
    {
        TextBlock _textBlock;
        double _fontSizePerHeight, _sidePaddingPerHeight;

        public BorderedTextBlock() : base()
        {
            Children.Add(_textBlock = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("MS Mincho"),
            });
        }
        #region IBorderedTextBlock impl
        public string Text 
        {
            get => _textBlock.Text;
            set => _textBlock.Text = value; 
        }

        protected override void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.OnSizeChanged(sender, e);
            UpdateFontSize();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            UpdateFontSize();
            OnSharedGeometryChanged(
                this,
                new System.ComponentModel.PropertyChangedEventArgs(
                    nameof(ISharedGeometryConsumer.SharedBorderThickness)));
        }
        void UpdateFontSize()
        {
            _textBlock.FontSize = _fontSizePerHeight * RealHeight(1.0, 0);
        }
        public double FontSizePerHeight
        {
            set
            {
                _fontSizePerHeight = value;
                UpdateFontSize();
            }
        }

        void SetPadding()
        {
            double borderHeight = RealHeight(1.0, 0);
            _textBlock.Padding = (_textBlock.TextAlignment == TextAlignment.Left) ?
                new Thickness(_sidePaddingPerHeight * borderHeight, 0, 0, 0) :
                new Thickness(0, 0, _sidePaddingPerHeight * borderHeight, 0);
        }
        public double SidePaddingPerHeight
        {
            set
            {
                _sidePaddingPerHeight = value;
                SetPadding();
            }                
        }
        public Microsoft.UI.Xaml.TextAlignment TextAlignment
        {
            set
            {
                _textBlock.TextAlignment = value;
                SetPadding();
            }
        }
        #endregion IBorderedTextBlock impl
        #region IMouseOperations impl
        public event PointerEventHandler PointerDown
        {
            add
            {
                _textBlock.PointerPressed += value;
            }
            remove
            {
                _textBlock.PointerPressed -= value;
            }
        }
        public event PointerEventHandler PointerUp
        {
            add
            {
                _textBlock.PointerReleased += value;
            }
            remove
            {
                _textBlock.PointerReleased -= value;
            }
        }
        #endregion IMouseOperations impl
    }
}
