using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUIC
{
    internal interface IMouseOperations
    {
        event PointerEventHandler PointerDown;

        event PointerEventHandler PointerUp;
    }
}
