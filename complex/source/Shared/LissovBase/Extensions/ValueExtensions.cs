using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase
{
    public static class ValueExtensions
    {
        public static void CopyFrom(this ModelBase.Value value, ModelBase.Value source, bool copyValue)
        {
            value.DisplayName = source.DisplayName;
            value.GroupName = source.GroupName;
            value.InitValue = source.InitValue;
            value.InitValueVisible = source.InitValueVisible;
            value.LinkModelName = source.LinkModelName;
            value.LinkValueName = source.LinkValueName;
            value.MaxValue = source.MaxValue;
            value.Measure = source.Measure;
            value.MinValue = source.MinValue;
            value.Name = source.Name;
            value.Type = source.Type;
            if (copyValue)
                value.Value = source.Value;
            value.Visible = source.Visible;
        }
    }
}
