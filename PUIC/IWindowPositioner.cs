namespace PUIC
{
    public interface IWindowPositioner
    {
        /// <summary>
        /// Align to the screen which has the specified aspect ratio
        /// </summary>
        /// <param name="wPerH">W / H of the target screen</param>
        /// <param name="rTol">relative tolerance of comparison of the aspect ratios</param>
        /// <returns>true: success, false: no adequate screen was found.</returns>
        bool AlignToScreen(double wPerH, double rTol);

        /// <summary>
        /// Create a new window covering the window implementing this interface.
        /// </summary>
        WindowBase Covering { get; }
    }
}
