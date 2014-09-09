using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using LissovLog;

namespace Model_Fluids
{
    public partial class FluidsModel
    {
        public override void BeforeCalculate()
        {
            base.BeforeCalculate();
            PressureVenous[0] = 4;
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                PressureVascular[i].Value[0] = 25;
                PressureVascularOsmotic[i].Value[0] = 25;
            }
            PressureVascular[3].Value[0] = 6;
            PressureVascularOsmotic[3].Value[0] = 6;

            CalculateGravityPressures(0, 0);
            CalculateExternalPressures(0, 0);
            CalculateBasic(0, 0);
            CalcTotals(0);
        }

        public override void Cycle()
        {
            CalculateGravityPressures(CurrStep + 1, CurrStep);
            CalculateExternalPressures(CurrStep + 1, CurrStep);
            CalculateBasic(CurrStep + 1, CurrStep);
            CalculateVolumes(CurrStep + 1, CurrStep);
            CalcTotals(CurrStep + 1);

            if (SimulateFluids.Value == LissovModelBase.FALSE)
            {
                for (int i = 0; i < configuration.CompartmentCount; i++)
                    FlowInterstitialVascular[i][CurrStep + 1] = 0;
                FlowLymphaOutput[CurrStep + 1] = 0;
            }
        }

        public void CalculateGravityPressures(int newstep, int basestep)
        {
            for (int i = 0; i < configuration.CompartmentCount; i++)
                PressureGravity[i][newstep] =
                    Constants.GRAVITY_TO_MMHG * Gravity[basestep] * Height[i] * Math.Sin(RotationAngle[basestep]);

            PressureGravityLymphatic[newstep] =
                Constants.GRAVITY_TO_MMHG * Gravity[basestep] * HeightLympha * Math.Sin(RotationAngle[basestep]);
        }

        public void CalculateExternalPressures(int newstep, int basestep)
        {
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                PressureExternalInterstitial[i][newstep] =
                    ElasticKoeffInterstitial[i] * PressureExternal[basestep];
                PressureExternalCell[i][newstep] =
                    ElasticKoeffCellular[i] * PressureExternal[basestep];
            }

            PressureExternalLymphatic[newstep] =
                ElasticKoeffLympha * PressureExternal[basestep];
        }

        public void CalculateBasic(int newstep, int basestep)
        {
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                PressureInter[i][newstep] = RigidityInterst[i]
                    * PV_interstitial[i].getValue(VolumeInter[i][basestep] - UnstressedInter[i])
                    + UnstressedPressureInterstitial;
                PressureCell[i][newstep] = RigidityCell[i] * PV_celular[i].getValue(VolumeCell[i][basestep] - UnstressedCell[i]);

                PressureOsmoticInterstital[i][newstep] = OsmoticInterst[i] / VolumeInter[i][basestep];
                PressureOsmoticCellular[i][newstep] = OsmoticCell[i] / VolumeCell[i][basestep];
            }
            PressureLympha[newstep] = RigidityLympha * pv_lympha.Content.getValue(VolumeLympha[basestep] - UnstressedLympha);
            PressureOsmoticLympha[newstep] = OsmoticLympha / VolumeLympha[basestep];

            if (UseFilteringRegulation.Value == LissovModelBase.TRUE
                && DistributeCFR.Value == LissovModelBase.FALSE)
                ResistanceVascularInterstKoeff[newstep] = 1 - FilteringResPressKoeff * (PressureVenous[basestep] - FilteringVenousPress0);
            else
                ResistanceVascularInterstKoeff[newstep] = 1;

            if (ResistanceVascularInterstKoeff[newstep] < 0.01)
            {
                Log.Write(LogLevel.WARN, "ResistanceVascularInterstKoeff on step [{0}] was below threshold [{1}] - set to threshold [{2}]", newstep, ResistanceVascularInterstKoeff[newstep], 0.01);
                ResistanceVascularInterstKoeff[newstep] = 0.01;                
            }

            FlowLymphaInput[newstep] = 0;
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                double vresk = ResistanceVascularInterstKoeff[newstep];
                if (UseFilteringRegulation.Value == LissovModelBase.TRUE
                && DistributeCFR.Value == LissovModelBase.TRUE)
                {
                    vresk = 1 - CFR_PressureGain[i] * (PressureVascular[i][basestep] - CFR_Pressure0[i]);
                }

                DeltaVolInterstitialVascular[i][newstep] = (PressureInter[i][newstep] - PressureVascular[i][basestep] + PressureExternalInterstitial[i][newstep]
                        - (PressureOsmoticInterstital[i][newstep] - PressureVascularOsmotic[i][basestep]));

                FlowInterstitialVascular[i][newstep] =
                        DeltaVolInterstitialVascular[i][newstep]
                        / (ResistanceInterstitialVascular[i] * vresk);
                
                FlowInterstitialCellular[i][newstep] = (PressureInter[i][newstep] - PressureCell[i][newstep]
                        + PressureExternalInterstitial[i][newstep] - PressureExternalCell[i][newstep]
                        + (PressureOsmoticCellular[i][newstep] - PressureOsmoticInterstital[i][newstep]))
                        / ResistanceInterstitialCellular[i];
                FlowInterstitialLympha[i][newstep] = (PressureInter[i][newstep] - PressureLympha[newstep]
                        + PressureExternalInterstitial[i][newstep] - PressureExternalLymphatic[newstep]
                        + PressureGravity[i][newstep] - PressureGravityLymphatic[newstep]
                        + (PressureOsmoticLympha[newstep] - PressureOsmoticInterstital[i][newstep]))
                        / ResistanceInterstitialLympha[i];

                FlowLymphaInput[newstep] += FlowInterstitialLympha[i][newstep];
            }

            FlowLymphaOutput[newstep] = (PressureLympha[newstep] + PressureExternalLymphatic[newstep] 
                        - PressureRightVentricle[basestep]) / ResistanceLymphaOut;
        }

        public void CalculateVolumes(int ns, int bs)
        {
            double step = (double)Step;
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                VolumeInter[i][ns] = VolumeInter[i][bs] -
                    (FlowInterstitialVascular[i][ns] + FlowInterstitialCellular[i][ns] + FlowInterstitialLympha[i][ns])
                    * step;
                VolumeCell[i][ns] = VolumeCell[i][bs] + FlowInterstitialCellular[i][ns] * step;
            }

            VolumeLympha[ns] = VolumeLympha[bs] + (FlowLymphaInput[ns] - FlowLymphaOutput[ns]) * step;
        }

        public void CalcTotals(int sn)
        {
            VolumeInterstitial[sn] = 0;
            VolumeCellular[sn] = 0;
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                VolumeInterstitial[sn] += VolumeInter[i][sn];
                VolumeCellular[sn] += VolumeCell[i][sn];
            }
            VolumeFluidsTotal[sn] = VolumeInterstitial[sn] + VolumeCellular[sn] + VolumeLympha[sn];

            TotalVascularExchange[sn] = FlowLymphaOutput[sn];
            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                TotalVascularExchange[sn] += FlowInterstitialVascular[i][sn];
            }

            if (sn > 0)
                VolumeTotal[sn] = VolumeFluidsTotal[sn] + CVSVolume[sn - 1] + KidneyTotalFlowout[sn - 1];
            else
                VolumeTotal[sn] = VolumeFluidsTotal[sn] + CVSVolume.InitValue;
        }
    }
}
