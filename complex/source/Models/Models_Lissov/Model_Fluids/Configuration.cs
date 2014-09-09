using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using System.Xml;
using LissovLog;

namespace Model_Fluids
{
    public class Configuration : ParameterSafe
    {
        private const string COMPARTMENT_COUNT = "CompartmentCount";
        private const string COMPARTMENT_NAMES = "CompartmentNames";
        private const string COMPARTMENT_NUMBER = "Compartment_";

        private FluidsModel _model;
        #region Construction
        public Configuration(string name, string displayName, FluidsModel model)
            : base(name, displayName)
        {
            _model = model;
        }

        public void LoadCVSConfiguration(string filename)
        {
            try
            {
                Model_CVS.NetConfiguration conf = Model_CVS.NetConfiguration.LoadFromFile(filename, null);

                List<string> compartments = new List<string>();
                for (int i = 0; i < conf.CompartmentCount; i++)
                {
                    string[] parts = conf.CompartmentNames[i].Split(' ');
                    if (parts[parts.Length - 1].ToLower().Contains("capillar"))
                    {
                        string nm = parts[0];
                        for (int j = 1; j < parts.Length - 1; j++) nm += " " + parts[j];
                        compartments.Add(nm);
                    }
                }

                CompartmentCount = compartments.Count;
                CompartmentNames = compartments.ToArray();
                /*CompartmentIDs = new string[CompartmentCount];
                for (int i = 0; i < CompartmentCount; i++)
                    CompartmentIDs[i] = CompartmentNames[i].ToIdentifier();*/

                _model.UpdateConfiguration();
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Error while loading configuration from CVS config file [C002]: " + ex.Message);
            }
        }
        #endregion

        public int CompartmentCount { get; private set; }
        public string[] CompartmentNames { get; private set; }
        //public string[] CompartmentIDs { get; private set; }

        public int GetCompartmentNumByName(string name)
        {
            var a = CompartmentNames.Select((c, index) => new { c, index }).Where(pair => pair.c == name);
            if (a.Count() == 1) 
                return a.First().index;
            else
                return -1;
        }

        public override XmlElement ToXml(XmlElement parentElement)
        {
            XmlElement elt = base.ToXml(parentElement);
            try
            {
                SetAttribute(elt, COMPARTMENT_COUNT, CompartmentCount);

                XmlElement xe = elt.CreateChildNode(COMPARTMENT_NAMES);
                for (int i = 0; i < CompartmentCount; i++)
                    SetAttribute(xe.CreateChildNode(COMPARTMENT_NUMBER + i.ToString()), "Name", CompartmentNames[i]);
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Error while saving configuration [C003]: " + ex.Message);
            }

            return elt;
        }
        
        public override object FromXml(XmlElement currentElement)
        {
            try
            {
                Value = 0; // to indicate that this is loaded

                base.FromXml(currentElement);
                bool changed = false;
                
                int cc = GetInteger(currentElement, COMPARTMENT_COUNT);
                if (cc == 0 || CompartmentCount != cc)
                {
                    CompartmentCount = cc; 
                    CompartmentNames = new string[CompartmentCount];                    
                    changed = true; 
                }
                
                //CompartmentIDs = new string[CompartmentCount];
                XmlElement xe = currentElement.GetChildNode(COMPARTMENT_NAMES);
                for (int i = 0; i < CompartmentCount; i++)
                {
                    string cn = GetString(xe.GetChildNode(COMPARTMENT_NUMBER + i.ToString()), "Name");
                    if (CompartmentNames[i] != cn)
                    {
                        CompartmentNames[i] = cn;
                        changed = true;
                    }
                    //CompartmentIDs[i] = CompartmentNames[i].ToIdentifier();
                }

                if (changed)
                    _model.UpdateConfiguration();
            }
            catch (Exception ex)
            {
                Log.Write(LogLevel.ERROR, "Error while loading configuration [C001]: " + ex.Message);
            }

            return this;
        }

    }
}
