using Microsoft.UI.Xaml;
using System.ComponentModel;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Controls;

namespace PUIC2
{
    // Container which is a direct child of a window.
    public interface ITopContainer : IPUIC
    {
        /// <summary>
        /// Attach a window and a container.
        /// In detail operation, the container stores the window in one of its field, _window and
        /// set itself in window.Content property.
        /// </summary>
        /// <param name="window"></param>
        void Attach(Window window);

        /// <summary>
        /// Get the corresponding AppWindow object.
        /// It is necessary to access AppWindow when you need to move or resize the window.
        /// </summary>
        AppWindow AppWindow { get; }

        /// <summary>
        /// Export itself as an interface.
        /// </summary>
        ISharedGeometryConsumer SharedGeometry { get; }
    }

    /// <summary>
    /// Specify a proportional coefficient proportional to width or height of an window.
    /// </summary>
    public enum ProportionalTo
    {
        Width,
        Height,
    }

    public interface ISharedGeometryProvider
    {
        /// <summary>
        /// Set the width of an AppWindow
        /// </summary>
        int ClientWidth { get; set; }

        /// <summary>
        /// Set the height of an AppWindow
        /// </summary>
        int ClientHeight { get; set; }

        /// <summary>
        /// You can fix the ratio of the width and height
        /// </summary>
        bool IsAspectRatioFixed { get; set; }

        double BorderThicknessPerClientLengthD { get; }

        double CornerRadiusPerClientLengthD { get; }

        /// <summary>
        /// (border thickness, reference window edge length)
        /// </summary>
        (int, int) BorderThicknessPerClientLength { get; set; }

        /// <summary>
        /// (corner radius, reference window edge length)
        /// </summary>
        (int, int) CornerRadiusPerClientLength { get; set; }

        /// <summary>
        /// Selecting width or height as a reference dimention
        /// of a border thickness and a corner radius.
        /// </summary>
        ProportionalTo ProportionalTo { get; set; }
    }

    public interface ISharedGeometryConsumer
    {
        double SharedWindowWidth { get; }

        double SharedWindowHeight { get; }

        double SharedBorderThickenss { get; }

        double SharedCornerRadius { get; }


        event PropertyChangedEventHandler Changed;
    }
}
