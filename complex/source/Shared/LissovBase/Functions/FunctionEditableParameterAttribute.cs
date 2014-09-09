using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase.Functions
{
    public enum EditorType { Default, Text, Double, Time}

    public class FunctionEditableParameterAttribute : Attribute
    {
        public string Title { get; set; }

        public string Unit { get; set; }

        public string GetUnit(Function f)
        {
            if (Unit == Constants.UnitsC.xunit)
                return f.XUnit;
            if (Unit.Equals(Constants.UnitsC.value))
                return f.Unit;
            return Constants.Units.GetLocalizedUnit(Unit);
        }

        private EditorType _editor = EditorType.Default;
        public EditorType Editor
        {
            get { return _editor; }
            set { _editor = value; }
        }

        public FunctionEditableParameterAttribute(string title, string unit)
        {
            Title = title;
            Unit = unit;
        }        
    }
}
