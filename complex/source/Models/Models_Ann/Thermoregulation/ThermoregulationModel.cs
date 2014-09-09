using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using LissovLog;
using System.Windows.Forms;
using LissovBase.Functions;

namespace Thermoregulation
{
    public class ThermoregulationModel : LissovModelBase
    {

       

    //    T,w,Vs,Ves,Ved,Hb,O2,Tb,Ts,Es,Ws,Res:array[1..16] of array of Real;
    //c,k,m,u,s,hc,hr,rad,L,koef,porog,ss:array[1..16] of Real;



        public override object FromXml(System.Xml.XmlElement currentElement)
        {
            GenerateConfiguration();

            return base.FromXml(currentElement);
        }

        private const int CompartmentCount = 5;
        private int FullCompartmentCount;
        private string[] CompartmentNames;
        private void GenerateConfiguration()
        {
            FullCompartmentCount = CompartmentCount * 2;
            CompartmentNames = new string[FullCompartmentCount];
            CompartmentNames[0] = "Head";
            CompartmentNames[1] = "Leg";
            CompartmentNames[2] = "Arm";
            CompartmentNames[3] = "Body";
            CompartmentNames[4] = "Neck";

            for (int i = 0; i < CompartmentCount; i++)
            {
                CompartmentNames[FullCompartmentCount - i - 1] = CompartmentNames[i] + " inner";
                CompartmentNames[i] = CompartmentNames[i] + " outer";
            }

            ProcessCollections(CompartmentNames);
            CollectLists();
        }

        /*public bool FindCVSModel()
        {
            foreach (var item in Configuration.Models)
            {
                if (item is CVSModel)
                {
                    if ((item as CVSModel).NetConfiguration == null)
                    {
                        Log.Write(LogLevel.ERROR, "Model CVS is not loaded, cannot load Thermoregulation");
                        return false;
                    }
                    else
                    {
                        netConfiguration = (item as CVSModel).NetConfiguration;
                        ProcessCollections(netConfiguration.CompartmentNames);
                        CollectLists();
                        Log.Write(LogLevel.INFO, "Model CVS found, configuration with {0} compartments loaded", netConfiguration.CompartmentCount);
                        return true;
                    }
                }
            }
            Log.Write(LogLevel.ERROR, "Model CVS is not found, cannot load Thermoregulation");
            return false;
        }*/

        #region Parameters

        public ParameterSafe rob = new ParameterSafe("rob");
        public ParameterSafe cb = new ParameterSafe("cb");
        public ParameterSafe Vb = new ParameterSafe("Vb");
        public ParameterSafe PTime = new ParameterSafe("PTime");
        public ParameterSafe Ts0 = new ParameterSafe("Ts0");
        public ParameterSafe koef1 = new ParameterSafe("Other koefficient");
        public ParameterSafe koef2 = new ParameterSafe("Another koefficient");
        public ParameterSafe koef3 = new ParameterSafe("Third unknown koefficient");

        public ParameterCollection c = new ParameterCollection("Capacity");
        public ParameterCollection k = new ParameterCollection("Koefficient");
        public ParameterCollection Mass = new ParameterCollection("Mass");
        public ParameterCollection u = new ParameterCollection("U");
        public ParameterCollection Square = new ParameterCollection("Square");
        public ParameterCollection Length = new ParameterCollection("Length");
        public ParameterCollection Radius = new ParameterCollection("Radius");
        public ParameterCollection hc = new ParameterCollection("Conduction koefficient");
        public ParameterCollection hr = new ParameterCollection("Radiation koefficient");
        public ParameterCollection threshold = new ParameterCollection("Temperature threshold");
        #endregion

        #region Variables

        public LissovValue Sum = new LissovValue("Sum", ModelBase.Value.ValueType.Output, Constants.Units.unit);
        public LissovValue ExternalTemperature = new LissovValue("External temperature", ModelBase.Value.ValueType.Input, Constants.Units.degreeCelsium);
        public LissovValue AverageSkinPemperature = new LissovValue("Average skin temperature", ModelBase.Value.ValueType.Output, Constants.Units.degreeCelsium);

        public ValueCollection<double> Temperature = new ValueCollection<double>("Temperature", ModelBase.Value.ValueType.Output, true, Constants.Units.degreeCelsium);
        public ValueCollection<double> BloodFlow = new ValueCollection<double>("Blood flow", ModelBase.Value.ValueType.Input, true, Constants.Units.ml_per_second);
        public ValueCollection<double> TempBlood = new ValueCollection<double>("Blood temperature", ModelBase.Value.ValueType.Output, true, Constants.Units.degreeCelsium);
        public ValueCollection<double> TempSkin = new ValueCollection<double>("Skin temperature", ModelBase.Value.ValueType.Output, true, Constants.Units.degreeCelsium);
        public ValueCollection<double> Resistance = new ValueCollection<double>("Resistance", ModelBase.Value.ValueType.Output, true, Constants.Units.resist);
        public ValueCollection<double> Evaporation = new ValueCollection<double>("Evaporation", ModelBase.Value.ValueType.Output, true, Constants.Units.joule);
        public ValueCollection<double> Energy = new ValueCollection<double>("Energy", ModelBase.Value.ValueType.Output, true, Constants.Units.joule);
        #endregion

        public ThermoregulationModel()
        {
            this.Name = "Thermoregulation";
            this.DisplayName = "Thermoregulation";
            this.Description = "Model of thermoregulation";
        }


        public override void Cycle()
        {
            base.Cycle();

            Sum[CurrentStep] = 0;
            for (int i = 0; i < FullCompartmentCount; i++)
            {
                Sum[CurrentStep] = Sum[CurrentStep]
                    + BloodFlow[i][CurrentStep] * rob.Value * cb.Value * (Temperature[i][CurrentStep - 1] - TempBlood[i][CurrentStep - 1]);
            }
            
            for (int i = 0; i < CompartmentCount; ++i)
            {
                double rstep = (1 / PTime.Value) * (double)Step * ((1 / (c[i] * Mass[i])) * (
                     Energy[i][CurrentStep - 1]
                     - 3.14 * k[i] * Radius[i] * Length[i] * (Temperature[i][CurrentStep - 1] - Temperature[FullCompartmentCount - i - 1][CurrentStep - 1])
                     - BloodFlow[i][CurrentStep - 1] * rob.Value * cb.Value * (Temperature[i][CurrentStep - 1] - TempBlood[i][CurrentStep - 1])
                     - (hc[i] + hr[i]) * Square[i] * (Temperature[i][CurrentStep - 1] - ExternalTemperature[CurrentStep - 1])                     
                     ));

                Temperature[i][CurrentStep] = Temperature[i][CurrentStep - 1] + rstep;

                TempBlood[i][CurrentStep] = TempBlood[i][CurrentStep - 1] +
                    (1 / PTime.Value) * (double)Step * (1 / (Vb.Value * rob.Value * cb.Value)) * (Sum[CurrentStep]);
            }

            AverageSkinPemperature[CurrentStep] = 0;
            for (int i = 0; i < CompartmentCount; i++)
            {
                AverageSkinPemperature[CurrentStep] += Temperature[i][CurrentStep] / CompartmentCount;
            }

            for (int i = 0; i < FullCompartmentCount; ++i)
            {

                TempSkin[i][CurrentStep] = AverageSkinPemperature[CurrentStep];
            }

            for (int i = 0; i < FullCompartmentCount; ++i)
            {

                if (TempSkin[i][CurrentStep] < Ts0.Value - 0.983)
                {
                    Evaporation[i][CurrentStep] = Evaporation[i][CurrentStep - 1] +
                        koef1 * (TempSkin[i][CurrentStep] - TempSkin[i][CurrentStep - 1]) +
                        koef2 * (TempBlood[i][CurrentStep] - TempBlood[i][CurrentStep - 1]);
                }
                else
                {
                    Evaporation[i][CurrentStep] = Evaporation[i][CurrentStep - 1];
                }

                Energy[i][CurrentStep] = Energy[i][0];
            }

            for (int i = 0; i < CompartmentCount; ++i)
            {
                Resistance[i][CurrentStep] = Resistance[i][0] + koef3 * (Temperature[i][0] - Temperature[i][CurrentStep]);

                BloodFlow[i][CurrentStep] = BloodFlow[i][0] / Resistance[i][CurrentStep];
            }            
        }

        private UserControl _control;
        public override UserControl GetControl()
        {
            if (_control == null)
                _control = new ThermoSetupControl(this);
            return _control;
        }
    }
}
        
        
        
        
        
        
   
