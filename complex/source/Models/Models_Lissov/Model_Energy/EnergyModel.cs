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
        public ParameterSafe AtfProductionK = new ParameterSafe("ATFProductionK", "ATF production koeff");
        public ParameterSafe AtfDestructionK = new ParameterSafe("ATFDestructionK", "ATF usage koeff");
        public ParameterSafe WorkLoad0 = new ParameterSafe("WorkLoad0", "Work load neutral");
        
        public LissovValue FlowSysCap = new LissovValue("System Cappillar flow", Value.ValueType.Input, Constants.Units.ml_per_second);
        public LissovValue MitochondriaArea = new LissovValue("Mitochondria area", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue Pyruvate = new LissovValue("Pyruvate", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue Oxygen = new LissovValue("Oxygen", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue WorkLoad = new LissovValue("Work load", Value.ValueType.Input, Constants.Units.unit);

        public LissovValue Energy = new LissovValue("Energy", Value.ValueType.Output, Constants.Units.unit);
        public LissovValue AtfProduction = new LissovValue("ATF production speed", Value.ValueType.Output, Constants.Units.unit);
        public LissovValue AtfDestruction = new LissovValue("ATF usage speed", Value.ValueType.Output, Constants.Units.unit);
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
            AtfProduction[ns] = AtfProductionK * MitochondriaArea[bs] * Pyruvate[bs] * Oxygen[bs] * FlowSysCap[bs];
            AtfDestruction[ns] = AtfDestructionK * (WorkLoad0 + WorkLoad[bs]);

            Energy[ns] = Energy[bs] + (double)Step * TimeParameter.Value * (AtfProduction[bs] - AtfDestruction[bs]);
        }
        #endregion
    }
}
