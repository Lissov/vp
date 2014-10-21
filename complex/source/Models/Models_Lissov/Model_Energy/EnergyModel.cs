using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using ModelBase;

namespace Model_Energy
{
    public class EnergyModel : LissovModelBase
    {
        public EnergyModel()
        {
            Name = "Energy";
            DisplayName = "Energy";
        }

        #region Vars
        public ParameterSafe TimeParameter = new ParameterSafe("TimeKoeff", "Time Koefficient");
        public ParameterSafe AtpProductionK = new ParameterSafe("ATPProductionK", "ATP production koeff");
        public ParameterSafe AtpDestructionK = new ParameterSafe("ATPDestructionK", "ATP usage koeff");
        public ParameterSafe WorkLoad0 = new ParameterSafe("WorkLoad0", "Work load neutral");
        
        public LissovValue FlowSysCap = new LissovValue("System Cappillar flow", Value.ValueType.Input, Constants.Units.ml_per_second);
        public LissovValue MitochondriaArea = new LissovValue("Mitochondria area", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue Pyruvate = new LissovValue("Pyruvate", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue Oxygen = new LissovValue("Oxygen", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue WorkLoad = new LissovValue("Work load", Value.ValueType.Input, Constants.Units.unit);

        public LissovValue Energy = new LissovValue("Energy", Value.ValueType.Output, Constants.Units.unit);
        public LissovValue AtpProduction = new LissovValue("ATP production speed", Value.ValueType.Output, Constants.Units.unit);
        public LissovValue AtpDestruction = new LissovValue("ATP usage speed", Value.ValueType.Output, Constants.Units.unit);
        #endregion
        
        #region Calcs
        public override void BeforeCalculate()
        {
            base.BeforeCalculate();
            //CalcStep(0, 0);
        }

        public override void Cycle(){
            CalcStep(CurrStep + 1, CurrStep);
        }

        private void CalcStep(int ns, int bs)
        {
            AtpProduction[ns] = AtpProductionK * MitochondriaArea[bs] * Pyruvate[bs] * Oxygen[bs] * FlowSysCap[bs];
            AtpDestruction[ns] = AtpDestructionK * (WorkLoad0 + WorkLoad[bs]);

            Energy[ns] = Energy[bs] + (double)Step * TimeParameter.Value * (AtpProduction[bs] - AtpDestruction[bs]);
        }
        #endregion
    }
}
