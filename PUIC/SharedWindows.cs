using System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace PUIC
{
    public class SharedWindows : Dictionary<Microsoft.UI.WindowId, Window>
    {
        public static SharedWindows TheInstance;

        static SharedWindows()
        {
            TheInstance = new SharedWindows();
        }
    }
}
