using System;

namespace PUIC2
{
    /// <summary>
    /// The common base interface of PUIC (Portable UI Component) classes.
    /// </summary>
    public interface IPUIC
    {
        /// <summary>
        /// A type of a base class which is defined in Microsoft.Xaml.UI or Microsoft.Xaml.UI.Controls
        /// namespace.
        /// </summary>
        Type DerivedFrom { get; }
    }
}
