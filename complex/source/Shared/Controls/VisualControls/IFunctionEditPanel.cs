using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase.Functions;

namespace VisualControls
{
    public interface IFunctionEditPanel
    {
        void Init(Function function);
        bool EditorsVisible { get; set; }
        void RedrawGraph();
    }
}
