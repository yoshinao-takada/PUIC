using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;

namespace PUIC
{
    public interface IPUICObject : ISharedGeometryConsumer
    {
        ISharedGeometryConsumer SharedGeometry { get; set; }

        IPUICObject PUICParent { get; set; }
    }

    /// <summary>
    /// Basic structure for making child object geometry proportional to window height.
    /// The child side interface of the structure.
    /// </summary>
    public interface ISharedGeometryConsumer
    {
        double SharedBorderThickness { get; }

        double SharedCornerRadius { get; }

        event EventHandler<PropertyChangedEventArgs> SharedGeometryChanged;
    }

    /// <summary>
    /// Basic structure for making child object geometry proportional to window height.
    /// The root window side interface of the structure.
    /// </summary>
    public interface ISharedGeometryProvider
    {
        double BorderThicknessPerHeight { set; }

        double CornerRadiusPerHeight { set; }

        double WidthPerHeight { set; }
    }
}
