using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValueCollection = LissovBase.ValueCollection<double>;
using LissovBase;
using LissovBase.Functions;
using ModelBase;

namespace Model_Fluids
{
    public partial class FluidsModel
    {
        #region Parameters
        public ParameterSafe SimulateFluids = new ParameterSafe("Simulate Fluids");
        public Configuration configuration;

        public ParameterCollection Height = new ParameterCollection("Height of ");
        public ParameterSafe HeightLympha = new ParameterSafe("Height of lymphatic department");
        public ParameterCollection ElasticKoeffInterstitial = new ParameterCollection("Elastic Koefficient Interstitial of ");
        public ParameterCollection ElasticKoeffCellular = new ParameterCollection("Elastic Koefficient Cellular of ");
        public ParameterSafe ElasticKoeffLympha = new ParameterSafe("Elastance koefficient of lymphatic department");
        public ParameterCollection RigidityInterst = new ParameterCollection("Rigidity of interstitia in ");
        public ParameterCollection RigidityCell = new ParameterCollection("Rigidity of cells in ");
        public ParameterSafe RigidityLympha = new ParameterSafe("Rigidity of lymphatic department");
        public ParameterCollection<PVFunction> PV_interstitial = new ParameterCollection<PVFunction>("PV dependency Interstitial for ");
        public ParameterCollection<PVFunction> PV_celular = new ParameterCollection<PVFunction>("PV dependency Cellular for ");
        public ParameterObject<PVFunction> pv_lympha = new ParameterObject<PVFunction>("PV dependency for lymphatic department", new PVFunction());
        public ParameterCollection UnstressedInter = new ParameterCollection("Unstressed volume Interstitial of ");
        public ParameterCollection UnstressedCell = new ParameterCollection("Unstressed volume Cellular of ");
        public ParameterSafe UnstressedLympha = new ParameterSafe("Unstressed volume of Lympha");
        public ParameterSafe UnstressedPressureInterstitial = new ParameterSafe("Unstressed pressure of interstitial tissues");
        public ParameterCollection OsmoticInterst = new ParameterCollection("Osmotic mass interstitial in ");
        public ParameterCollection OsmoticCell = new ParameterCollection("Osmotic mass cellular in ");
        public ParameterSafe OsmoticLympha = new ParameterSafe("Osmotic mass of Lympha");
        public ParameterSafe FilteringResPressKoeff = new ParameterSafe("Filtering resistance to venous pressure coefficient");
        public ParameterSafe FilteringVenousPress0 = new ParameterSafe("Venous pressure filtering threshold");
        public ParameterSafe UseFilteringRegulation = new ParameterSafe("Use Filtering Regulation");
        public ParameterCollection ResistanceInterstitialVascular = new ParameterCollection("Resistance between interstitial and vascular of ");
        public ParameterCollection ResistanceInterstitialCellular = new ParameterCollection("Resistance between interstitial and cellular of ");
        public ParameterCollection ResistanceInterstitialLympha = new ParameterCollection("Resistance between interstitial and lympha of ");
        public ParameterSafe ResistanceLymphaOut = new ParameterSafe("Resistance between Lympha and ventricle");

        public ParameterSafe DistributeCFR = new ParameterSafe("Distribute Capillary Filtration Rate change");
        public ParameterCollection CFR_Pressure0 = new ParameterCollection("CFR normal pressure of ");
        public ParameterCollection CFR_PressureGain = new ParameterCollection("CFR pressure gain of ");
        #endregion

        #region Inputs
        public ValueCollection PressureVascular = new ValueCollection("Vascular pressure in ", Value.ValueType.Input, true, Constants.Units.mmHg);
        public ValueCollection PressureVascularOsmotic = new ValueCollection("Osmotic pressure in ", Value.ValueType.Input, true, Constants.Units.mmHg);
        public LissovValue PressureRightVentricle = new LissovValue("PressureRightVentricle", "Pressure in right ventricle", Value.ValueType.Input, Constants.Units.mmHg);
        public LissovValue PressureVenous = new LissovValue("PressureVenous", "Venous pressure", Value.ValueType.Input, Constants.Units.mmHg);
        public LissovValue PressureExternal = new LissovValue("PressureExternal", "External pressure", Value.ValueType.Input, Constants.Units.mmHg);
        public LissovValue RotationAngle = new LissovValue("RotationAngle", "Angle", Value.ValueType.Input, Constants.Units.degree);
        public LissovValue Gravity = new LissovValue("Gravity", "Gravity", Value.ValueType.Input, Constants.Units.unit);
        public LissovValue CVSVolume = new LissovValue("CVS Volume", Value.ValueType.Input) { InitValue = 5200 };
        public LissovValue KidneyTotalFlowout = new LissovValue("Kidney volume output balance", Value.ValueType.Input);
        #endregion

        #region Outputs
        public ValueCollection PressureGravity = new ValueCollection("Gravity-caused pressure in ", Value.ValueType.Output, true, Constants.Units.mmHg);
        public LissovValue PressureGravityLymphatic = new LissovValue("Gravity-caused pressure of Lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.mmHg);
        public ValueCollection PressureExternalInterstitial = new ValueCollection("External interstitial pressure in ", Value.ValueType.Output, true, Constants.Units.mmHg);
        public ValueCollection PressureExternalCell = new ValueCollection("External cellular pressure in ", Value.ValueType.Output, true, Constants.Units.mmHg);
        public LissovValue PressureExternalLymphatic = new LissovValue("External-caused pressure of Lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.mmHg);
        public ValueCollection VolumeInter = new ValueCollection("Interstitial Volume of ", Value.ValueType.Output, true, Constants.Units.ml);
        public ValueCollection VolumeCell = new ValueCollection("Cellular Volume of ", Value.ValueType.Output, true, Constants.Units.ml);
        public ValueCollection PressureInter = new ValueCollection("Interstitial Pressure of ", Value.ValueType.Output, true, Constants.Units.mmHg);
        public ValueCollection PressureCell = new ValueCollection("Cellular Pressure of ", Value.ValueType.Output, true, Constants.Units.mmHg);
        public LissovValue VolumeLympha = new LissovValue("Volume of Lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.ml);
        public LissovValue PressureLympha = new LissovValue("Pressure of Lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.mmHg);
        public ValueCollection PressureOsmoticInterstital = new ValueCollection("Osmotic Interstitial Pressure of ", Value.ValueType.Output, true, Constants.Units.ml);
        public ValueCollection PressureOsmoticCellular = new ValueCollection("Osmotic Cellular Pressure of ", Value.ValueType.Output, true, Constants.Units.mmHg);
        public LissovValue PressureOsmoticLympha = new LissovValue("Osmotic pressure of Lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.mmHg);
        public LissovValue ResistanceVascularInterstKoeff = new LissovValue("Koefficient of Vascular-Interstitial resistance", Value.ValueType.Output, Constants.Units.none);

        public LissovValue FlowLymphaInput = new LissovValue("Input flow to lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.ml_per_second);
        public LissovValue FlowLymphaOutput = new LissovValue("Output flow from lympha", Value.ValueType.Output, "Lympha", true, Constants.Units.ml_per_second) { LinkExpected = true, NoInterpolation = true };
        public ValueCollection DeltaVolInterstitialVascular = new ValueCollection("Delta vol Interstitial to Vascular", Value.ValueType.Output, true, Constants.Units.mmHg, true, true);
        public ValueCollection FlowInterstitialVascular = new ValueCollection("Flow from Interstitial to Vascular of ", Value.ValueType.Output, true, Constants.Units.ml_per_second, true, true);
        public ValueCollection FlowInterstitialCellular = new ValueCollection("Flow from Interstitial to Cellular of ", Value.ValueType.Output, true, Constants.Units.ml_per_second);
        public ValueCollection FlowInterstitialLympha = new ValueCollection("Flow from Interstitial to Lymphatic of ", Value.ValueType.Output, true, Constants.Units.ml_per_second);

        public LissovValue VolumeInterstitial = new LissovValue("Interstitial Volume", Value.ValueType.Output, Constants.Units.ml);
        public LissovValue VolumeCellular = new LissovValue("Cellular Volume", Value.ValueType.Output, Constants.Units.ml);
        public LissovValue VolumeFluidsTotal = new LissovValue("Total Fluids model Volume", Value.ValueType.Output, Constants.Units.ml);
        public LissovValue TotalVascularExchange = new LissovValue("Total exchange with vascular system", Value.ValueType.Output, Constants.Units.ml_per_second);
        public LissovValue VolumeTotal = new LissovValue("Total Volume", Value.ValueType.Output);
        #endregion

        public void UpdateConfiguration()
        {
            int cc = configuration.CompartmentCount;
            string[] compnames = configuration.CompartmentNames;

            ProcessCollections(compnames);
            CollectLists();
        }
    }
}
