using Microsoft.UI.Xaml.Input;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;

namespace PUIC2
{
    /// <summary>
    /// Attach/Detach functions to a TopContainer object.
    /// </summary>
    public interface IUiControl : IPUIC
    {
        /// <summary>
        /// Attach to an ancestor object in a UI object tree.
        /// </summary>
        /// <param name="ancestor">object to attach</param>
        void Attach(IPUIC ancestor);

        /// <summary>
        /// Detach from the current ancestor.
        /// </summary>
        void Detach();

        /// <summary>
        /// SharedGeometry obtained from ITopContainer.
        /// </summary>
        ISharedGeometryConsumer SharedGeometry { get; set; }
    }

    /// <summary>
    /// Color settings for Bordered controls
    /// </summary>
    public interface IBordered
    {
        /// <summary>
        /// [0]: Border color, [1]: Background color, [2...]: Foreground or something
        /// </summary>
        Windows.UI.Color[] Colors { set; }
        /// <summary>
        /// [Left | Top | Righth | Bottom] = [1.0 | 0.0],
        /// 1.0 shows the edge.
        /// 0.0 hides the edge.
        /// </summary>
        Thickness EdgeMask { get; set; }

        /// <summary>
        /// [TopLeft | TopRight | BottomRight | BottomLeft] = [1.0 | 0.0]
        /// 1.0 makes the corner rounded.
        /// 0.0 does not make the corner rounded.
        /// </summary>
        CornerRadius CornerMask { get; set; }

        /// <summary>
        /// True sets all edges shown. false sets all edges hidden.
        /// </summary>
        bool AllEdges { set; }

        /// <summary>
        /// True makes all corners rouned. false sets all corners unrounded.
        /// </summary>
        bool AllCorners { set; }

    }

    public class InputEventArgs
    {
        /// <summary>
        /// The original event caused this event
        /// </summary>
        EventArgs PointerEvent { get; set; }

        /// <summary>
        /// Key code assigned to the software button of PUIC framework if applicable.
        /// </summary>
        UIKeyCode Code { get; set; }
    }

    public interface IUiInputControl
    {
        /// <summary>
        /// 
        /// </summary>
        UIKeyCode Code { get; set; }

        event EventHandler<InputEventArgs> PointerEvent;
    }
}
