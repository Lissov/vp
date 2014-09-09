using System;
using System.Collections.Generic;
using System.Text;
using Model_CVS;

namespace LissovBase.DynamicValue
{
    public class DynamicValueResistance : LissovValue, IDynamicValue
    {
        private int _dep1;
        private int _dep2;
        private int _middle;
        private CVSModel _model;        

        public DynamicValueResistance(CVSModel model, int dep1, int dep2, int middle)
            :base(string.Format("Resistance [{0}] to [{1}] through [{2}]",
                model.configuration.CompartmentNames[dep1], 
                model.configuration.CompartmentNames[dep2], 
                model.configuration.CompartmentNames[middle]), ValueType.Output)
        {
            Measure = LissovBase.Constants.Units.PRU;
            LinkExpected = false;
            this.GroupName = "Virtual";

            _model = model;
            _dep1 = dep1;
            _dep2 = dep2;
            _middle = middle;
            d1 = _model.configuration.CompartmentNames[dep1].ToIdentifier();
            d2 = _model.configuration.CompartmentNames[dep2].ToIdentifier();
            mid = _model.configuration.CompartmentNames[_middle].ToIdentifier(); 
        }

        private string d1;
        private string d2;
        private string mid;
        public double GetValueByTime(decimal time)
        {
            if (time > _model.CurrentTime)
                return 0;            
            double p1 = _model.GetValueByNameAndTime("Pressure"+ d1, time);
            double p2 = _model.GetValueByNameAndTime("Pressure"+ d2, time);
            double f = _model.GetValueByNameAndTime("FlowIn"+ mid, time);

            return f > 0 ? (p1 - p2) / f : 0;
        }

    }
}
