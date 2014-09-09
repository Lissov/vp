using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovBase;
using LissovLog;
using ModelBase;
using System.Xml;
using LissovBase.Functions;

namespace Model_Fluids
{
    public partial class FluidsModel : LissovModelBase
    {
        public FluidsModel()
        {
            Log.Write(LogLevel.INFO, "Model created");

            Name = "Fluids";
            DisplayName = "Fluids of human body";

            VP_OLE_GUID = "{0B8E314D-A186-4D2E-87BC-A718FF8D948F}";

            configuration = new Configuration("Configuration", "Configuration", this);
        }

        #region Visuals
        private FluidsControl _control = null;
        public override System.Windows.Forms.UserControl GetControl()
        {
            if (_control == null) _control = new FluidsControl(this);
            return _control;
        }
        #endregion

        #region Load/Save
        public override object FromXml(System.Xml.XmlElement currentElement)
        {
            XmlElement x = currentElement.GetChildNode("Parameters").GetChildNode("Configuration");
            configuration.FromXml(x.ChildNodes[0] as XmlElement);
            return base.FromXml(currentElement);
        }

        public override void LoadOLEFromParametersNode(System.Xml.XmlElement xml)
        {
            int all = xml.ChildNodes.Count;
            Log.Write(LogLevel.INFO, "Parameters count to load: " + all.ToString());
            int errors = 0;
            foreach (XmlElement item in xml.ChildNodes)
            {
                string name = item.Attributes[0].Value;
                string value = item.Attributes[1].Value;
                double d;
                double.TryParse(value, out d);

                #region Compartments
                string[] parts = name.Split(' ');
                if (parts != null && parts.Length > 1 && (parts[1] == "capillares" || parts[1] == "cappilares"))
                {
                    string pn = string.Empty;
                    for (int i = 3; i < parts.Length; i++)
                        pn += (i != 3 ? " " : string.Empty) + parts[i];

                    if (parts[0] == "Head") parts[0] = "Brain";
                    int cnum = configuration.GetCompartmentNumByName(parts[0]);
                    if (cnum < 0)
                    {
                        Log.Write(LogLevel.WARN, string.Format("Ignored parameter [{0}], value [{1}] : compartment [{2}] not found", name, value, parts[0]));
                        errors++;
                        continue;
                    }
                    switch (pn)
                    {
                        case "Height": Height[cnum] = d; continue;
                        case "unstressed volume-cellular": UnstressedCell[cnum] = d; continue;
                        case "rigidity-interstitial": RigidityInterst[cnum] = d; continue;
                        case "elastic coefficient interstitial": ElasticKoeffInterstitial[cnum] = d; continue;
                        case "elastic coefficient cellular": ElasticKoeffCellular[cnum] = d; continue;
                        case "interstitial osmotic ion count": OsmoticInterst[cnum] = d; continue;
                        case "cellular osmotic ion count": OsmoticCell[cnum] = d; continue;

                        case "interstitial PV linear P": PV_interstitial[cnum].linearP = d; continue;
                        case "interstitial PV linear V": PV_interstitial[cnum].linearV = d; continue;
                        case "interstitial PV level 1": PV_interstitial[cnum].level1 = d; continue;
                        case "interstitial PV level 2": PV_interstitial[cnum].level2 = d; continue;
                        case "interstitial PV power 1": PV_interstitial[cnum].power1 = d; continue;
                        case "interstitial PV power 2": PV_interstitial[cnum].power2 = d; continue;

                        case "cellular PV linear P": PV_celular[cnum].linearP = d; continue;
                        case "cellular PV linear V": PV_celular[cnum].linearV = d; continue;
                        case "cellular PV level 1": PV_celular[cnum].level1 = d; continue;
                        case "cellular PV level 2": PV_celular[cnum].level2 = d; continue;
                        case "cellular PV power 1": PV_celular[cnum].power1 = d; continue;
                        case "cellular PV power 2": PV_celular[cnum].power2 = d; continue;

                        case "initial volume-interstitial": VolumeInter[cnum].InitValue = d; continue;
                        case "initial volume-cellular": VolumeCell[cnum].InitValue = d; continue;
                        case "unstressed volume-interstitial": UnstressedInter[cnum] = d; continue;
                        case "rigidity-cellular": RigidityCell[cnum] = d; continue;
                        case "resistance vascule-interstitial": ResistanceInterstitialVascular[cnum] = d; continue;
                        case "resistance interstitial-cellular": ResistanceInterstitialCellular[cnum] = d; continue;
                        case "resistance interstitial-lymphatic": ResistanceInterstitialLympha[cnum] = d; continue;
                            
                        default:
                            Log.Write(LogLevel.WARN, string.Format("Ignored parameter [{0}], value [{1}] - parameter [{2}] not found", name, value, pn));
                            errors++;
                            continue;
                    }
                }
                #endregion

                switch (name)
                {
                    case "Step": Step = (decimal)d; continue;

                    case "Lymphatic Height": HeightLympha.Value = d; continue;
                    case "Lymphatic department - volume": VolumeLympha.InitValue = d; continue;
                    case "Lymphatic department - unstressed volume": UnstressedLympha.Value = d; continue;
                    case "Lymphatic department - rigidity": RigidityLympha.Value = d; continue;
                    case "Lymphatic department - resistance out": ResistanceLymphaOut.Value = d; continue;
                    case "Lymphatic department - elastic coefficient": ElasticKoeffLympha.Value = d; continue;
                    case "Lymphatic department - osmotic ion count": OsmoticLympha.Value = d; continue;

                    case "Lymphatic department - PV linear V": pv_lympha.Content.linearV = d; continue;
                    case "Lymphatic department - PV linear P": pv_lympha.Content.linearP = d; continue;
                    case "Lymphatic department - PV level 1": pv_lympha.Content.level1 = d; continue;
                    case "Lymphatic department - PV level 2": pv_lympha.Content.level2 = d; continue;
                    case "Lymphatic department - PV power 1": pv_lympha.Content.power1 = d; continue;
                    case "Lymphatic department - PV power 2": pv_lympha.Content.power2 = d; continue;

                    case "Unstreasses pressure interstitial": UnstressedPressureInterstitial.Value = d; continue;
                    case "ResVI_VenousPress - koeff": FilteringResPressKoeff.Value = d; continue;

                    case "Sleep time":
                    case "Exposure":
                        Log.Write(LogLevel.WARN, string.Format("Ignored parameter [{0}], value [{1}]", name, value));
                        all--;
                        continue;

                    default:
                        Log.Write(LogLevel.WARN, string.Format("Ignored unknown parameter [{0}], value [{1}]", name, value));
                        errors++;
                        continue;
                }
            }

            for (int i = 0; i < configuration.CompartmentCount; i++)
            {
                PV_interstitial[i].Update();
                PV_celular[i].Update();
            }

            Log.Write(LogLevel.INFO, "Successfully loaded: {0} of {1} ({2} %)", (all - errors).ToString(), all.ToString(), (100 * (all - errors) / all).ToString());
        }
        #endregion
    }
}