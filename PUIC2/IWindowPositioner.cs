namespace PUIC2
{
    public interface IWindowPositioner
    {
        /// <summary>
        /// Align to the screen which has the specified aspect ratio
        /// </summary>
        /// <param name="wPerH">W / H of the target screen</param>
        /// <param name="rTol">relative tolerance of comparison of the aspect ratios</param>
        /// <param name="index">ordinal number selecting one of same size display areas</param>
        /// <returns>true: success, false: no adequate screen was found.</returns>
        bool AlignToScreen(double wPerH, double rTol, int index);

        void Resize(double w, double h);
    }
}
