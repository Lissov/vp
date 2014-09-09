using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase
{
    public interface IDynamicValue
    {
        double GetValueByTime(decimal time);
    }
}
