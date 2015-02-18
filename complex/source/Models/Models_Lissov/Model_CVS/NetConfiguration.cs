using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using LissovBase;
using LissovLog;

namespace Model_CVS
{
    public class NetConfiguration
    {
        public string Name { get; set; }
        public string Comments { get; set; }

        private int _compartmentCount;
        public int CompartmentCount
        {
            get { return _compartmentCount; }
            set {
                if (value < 0) throw new ArgumentException("CompartmentCount must be >= 0");
                if (_compartmentCount == value) return;
                _compartmentCount = value;
                CompartmentNames = new string[_compartmentCount];
                Links = new int[_compartmentCount][];

                CompartmentPositionX = new double?[_compartmentCount];
                CompartmentPositionY = new double?[_compartmentCount];
            }
        }

        private string[] _compartmentNames;
        public string[] CompartmentNames { get {return _compartmentNames;}
            set
            {
                _compartmentNames = value;
                CompartmentIDs = new string[_compartmentCount];                
                for (int i = 0; i < CompartmentCount; i++)
                    CompartmentIDs[i] = _compartmentNames[i].ToIdentifier();
            }
        }
        private string[] _compartmentIDs;
        public string[] CompartmentIDs { 
            get { return _compartmentIDs; } 
            set { _compartmentIDs = value; } 
        }

        //[XmlIgnore]
        public int[][] Links { get; set; }
        public int GetCompartmentNumByID(string id)
        {
            for (int i = 0; i < CompartmentIDs.Length; i++)
                if (CompartmentIDs[i] == id) return i;
            return -1;
        }
        public int GetCompartmentNumByName(string id)
        {
            for (int i = 0; i < _compartmentNames.Length; i++)
                if (_compartmentNames[i] == id) return i;
            return -1;
        }

        private int _departmentCount;
        public int DepartmentCount
        {
            get { return _departmentCount; }
            set {
                if (value < 0) throw new ArgumentException("DepartmentCount must me >= 0");
                if (_departmentCount == value) return;
                _departmentCount = value;
                DepartmentNames = new string[_departmentCount];
                //DepartmentIDs = new string[_departmentCount];
                Departments = new int[_departmentCount][];
            }
        }
        public string[] DepartmentNames { get; set; }

        //[XmlIgnore]
        public int[][] Departments { get; set; }

        //[XmlIgnore]
        public int RightBeforeHeart { get; set; }        
        //[XmlIgnore]
        public int RightAfterHeart { get; set; }
        //[XmlIgnore]
        public int LeftBeforeHeart { get; set; }
        //[XmlIgnore]
        public int LeftAfterHeart { get; set; }
        //[XmlIgnore]
        public int Kidney { get; set; }
        //[XmlIgnore]
        public int Brain { get; set; }
        //[XmlIgnore]
        public int BeforeBrain { get; set; }

        public int DepartmentPulmonary { get; set; }

        private double?[] _compartmentPositionX;
        public double?[] CompartmentPositionX
        {
            get { return _compartmentPositionX; }
            set { _compartmentPositionX = value; }
        }
        private double?[] _compartmentPositionY;
        public double?[] CompartmentPositionY
        {
            get { return _compartmentPositionY; }
            set { _compartmentPositionY = value; }
        }


        #region Wrappers to save with IDs
        /// <summary>
        /// Used to save into file with IDs
        /// </summary>
        public CompartmentArray[] LinksIds
        {
            get
            {
                return CompartmentArray.GetFromIntArray(Links, _compartmentIDs, _compartmentIDs);
            }
            set
            {
                if (value == null)
                {
                    Log.Write(LogLevel.WARN, "LinksIds is null");
                    return;
                }
                Links = CompartmentArray.ConvertToIntArray(value, _compartmentCount, _compartmentIDs, _compartmentIDs);
            }
        }

        private string[] GetDepartmentIDs()
        {
            string[] res = new string[DepartmentCount];
            for (int i = 0; i < DepartmentCount; i++)
                res[i] = DepartmentNames[i].ToIdentifier();
            return res;
        }

        public CompartmentArray[] DepartmentsDefinitions
        {
            get { return CompartmentArray.GetFromIntArray(Departments, GetDepartmentIDs(), _compartmentIDs); }
            set { Departments = CompartmentArray.ConvertToIntArray(value, _departmentCount, GetDepartmentIDs(), _compartmentIDs); }
        }

        public string RightBeforeHeartID
        {
            get { return _compartmentIDs[RightBeforeHeart]; }
            set { RightBeforeHeart = GetCompartmentNumByID(value); }
        }
        public string RightAfterHeartID
        {
            get { return _compartmentIDs[RightAfterHeart]; }
            set { RightAfterHeart = GetCompartmentNumByID(value); }
        }
        public string LeftBeforeHeartID
        {
            get { return _compartmentIDs[LeftBeforeHeart]; }
            set { LeftBeforeHeart = GetCompartmentNumByID(value); }
        }
        public string LeftAfterHeartID
        {
            get { return _compartmentIDs[LeftAfterHeart]; }
            set { LeftAfterHeart = GetCompartmentNumByID(value); }
        }
        public string KidneyID
        {
            get { return _compartmentIDs[Kidney]; }
            set { Kidney = GetCompartmentNumByID(value); }
        }
        public string BrainID
        {
            get { return _compartmentIDs[Brain]; }
            set { Brain = GetCompartmentNumByID(value); }
        }
        public string BeforeBrainID
        {
            get { return _compartmentIDs[BeforeBrain]; }
            set { BeforeBrain = GetCompartmentNumByID(value); }
        }
        #endregion


        public VirtualCompartment[] virtualCompartments = new VirtualCompartment[0];

        public static NetConfiguration LoadFromFile(string filename, CVSModel model)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(NetConfiguration));
                    NetConfiguration config = ser.Deserialize(new StreamReader(filename)) as NetConfiguration;
                    
                    #region Numeric Links compatibility
                    if (config.Links[0] == null)
                    {
                        Log.Write(LogLevel.INFO, "Configuration file is in old format, that does not support String IDs. Reloading in Numbers.");
                        //reload as older version
                        XmlAttributeOverrides overs = new XmlAttributeOverrides();
                        overs.Add(config.GetType(), "Links", new XmlAttributes() { XmlIgnore = false });
                        overs.Add(config.GetType(), "LinksIDs", new XmlAttributes() { XmlIgnore = true });
                        overs.Add(config.GetType(), "Departments", new XmlAttributes() { XmlIgnore = false });
                        overs.Add(config.GetType(), "DepartmentsDefinitions", new XmlAttributes() { XmlIgnore = true });
                        string[] props = new string[] { "RightBeforeHeart", "RightAfterHeart", "LeftBeforeHeart", "LeftAfterHeart", "Kidney", "Brain", "BeforeBrain" };
                        foreach (var item in props)
                        {
                            overs.Add(config.GetType(), item, new XmlAttributes() { XmlIgnore = false });
                            overs.Add(config.GetType(), item + "ID", new XmlAttributes() { XmlIgnore = true });                            
                        }
                        ser = new XmlSerializer(typeof(NetConfiguration), overs);
                        config = ser.Deserialize(new StreamReader(filename)) as NetConfiguration;
                    } 
                    #endregion

                    foreach (var item in config.virtualCompartments)
                        item.Model = model;
                    if (model != null)
                        model.UpdateArraysLengthsToConfiguration(config);
                    return config;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new NetConfiguration() { Name = "Empty", CompartmentCount = 0 };
            }
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                XmlSerializer ser = new XmlSerializer(typeof(NetConfiguration));
                ser.Serialize(sw, this);
            }
        }
        public void LoadFromOldFile(string filename, CVSModel model)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    XmlReader reader = XmlReader.Create(sr);

                    do reader.Read(); while (!reader.EOF && reader.Name != "Organs");
                    CompartmentCount = int.Parse((string)reader.GetAttribute("OrgCount"));
                    do reader.Read(); while (!reader.EOF && reader.Name != "OrgNames");
                    for (int i = 0; i < CompartmentCount; i++)
                    {
                        CompartmentNames[i] = reader.GetAttribute(i);
                        CompartmentIDs[i] = CompartmentNames[i].ToIdentifier(true);
                    }

                    do reader.Read(); while (!reader.EOF && reader.Name != "Departments");
                    DepartmentCount = int.Parse((string)reader.GetAttribute("DepCount"));
                    do reader.Read(); while (!reader.EOF && reader.Name != "DepNames");
                    for (int i = 0; i < DepartmentCount; i++)
                    {
                        DepartmentNames[i] = reader.GetAttribute(i);
                        //DepartmentIDs[i] = GetIDFromName(DepartmentNames[i]);
                    }
                    do reader.Read(); while (!reader.EOF && reader.Name != "DepartmentSets");
                    for (int i = 0; i < DepartmentCount; i++)
                    {
                        do reader.Read(); while (!reader.EOF && reader.Name != "Dep_" + i.ToString());
                        List<int> depins = new List<int>();
                        for (int j = 0; j < reader.AttributeCount; j++)
                            depins.Add(int.Parse(reader.GetAttribute("Organ_" + j.ToString())));
                        Departments[i] = depins.ToArray();
                    }

                    do reader.Read(); while (!reader.EOF && reader.Name != "Links");
                    for (int i = 0; i < CompartmentCount; i++)
                    {
                        do reader.Read(); while (!reader.EOF && reader.Name != "Org_" + i.ToString());
                        int count = int.Parse(reader.GetAttribute("count"));
                        Links[i] = new int[count];
                        for (int j = 0; j < count; j++)
                            Links[i][j] = int.Parse(reader.GetAttribute("link_" + j.ToString()));
                    }

                    do reader.Read(); while (!reader.EOF && reader.Name != "MainOrgans");
                    RightBeforeHeart = int.Parse(reader.GetAttribute("RightAuricle"));
                    RightAfterHeart = int.Parse(reader.GetAttribute("PulmonaryArterie"));
                    LeftBeforeHeart = int.Parse(reader.GetAttribute("LeftAuricle"));
                    LeftAfterHeart = int.Parse(reader.GetAttribute("CurveAorta"));
                    Kidney = int.Parse(reader.GetAttribute("KidneyArterie"));
                    Brain = int.Parse(reader.GetAttribute("Brain"));
                }

                if (model != null)
                    model.UpdateArraysLengthsToConfiguration(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void mergeCompartment(int dest, int merger)
        {
            Log.Write(LogLevel.INFO, "Merging configuration");

            _compartmentCount--;
            ModelHelper.SetArrayLength<string>(ref _compartmentNames, _compartmentCount, merger);
            ModelHelper.SetArrayLength<string>(ref _compartmentIDs, _compartmentCount, merger);

            if (!Links[dest].Contains(merger))
                Log.Write(LogLevel.WARN, "Merging non-linked compartments");

            #region Links
            Log.Write(LogLevel.INFO, "Merging links");
            int[][] newLinks = new int[_compartmentCount][];
            int index = 0;
            for (int i = 0; i < _compartmentCount + 1; i++)
            {
                if (i != merger)
                {
                    List<int> lks = new List<int>();

                    //Copy of links
                    for (int j = 0; j < Links[i].Length; j++)
                    {
                        // if merger - add destination instead
                        int deptn = -1;
                        if (Links[i][j] == merger)
                        {
                            if (i != dest) deptn = dest;
                        }
                        else
                            deptn = Links[i][j];
                        if (deptn > merger) 
                            deptn--;
                        if (deptn >= 0 && !lks.Contains(deptn))
                            lks.Add(deptn);
                    }
                    if (i == dest)
                    {
                        // copy all destination to merger
                        for (int j = 0; j < Links[merger].Length; j++)
                        {
                            int deptn = Links[merger][j];
                            if (deptn == dest) continue;
                            if (deptn > merger)
                                deptn--;
                            if (!lks.Contains(deptn))
                                lks.Add(deptn);
                        }
                    }

                    newLinks[index] = lks.ToArray();
                    index++;
                }
            }
            Links = newLinks; 
            #endregion

            #region Departments
            Log.Write(LogLevel.INFO, "Removing merger from Departments");
            for (int i = 0; i < _departmentCount; i++)
            {
                List<int> dep = new List<int>();
                for (int j = 0; j < Departments[i].Length; j++)
                {
                    if (Departments[i][j] < merger)
                        dep.Add(Departments[i][j]);
                    else if (Departments[i][j] > merger)
                        dep.Add(Departments[i][j] - 1);
                }
                Departments[i] = dep.ToArray();
            } 
            #endregion

            #region External linked
            if (RightBeforeHeart == merger)
                Log.Write(LogLevel.ERROR, "Merger is external linked compartment");
            else
                if (RightBeforeHeart > merger) RightBeforeHeart--;
            if (LeftBeforeHeart == merger)
                Log.Write(LogLevel.ERROR, "Merger is external linked compartment");
            else
                if (LeftBeforeHeart > merger) LeftBeforeHeart--;
            if (RightAfterHeart == merger)
                Log.Write(LogLevel.ERROR, "Merger is external linked compartment");
            else
                if (RightAfterHeart > merger) RightAfterHeart--;
            if (LeftAfterHeart == merger)
                Log.Write(LogLevel.ERROR, "Merger is external linked compartment");
            else
                if (LeftAfterHeart > merger) LeftAfterHeart--;
            if (RightBeforeHeart == merger)
                Log.Write(LogLevel.ERROR, "Merger is external linked compartment");
            else
                if (RightBeforeHeart > merger) RightBeforeHeart--;
            if (Kidney == merger)
                Log.Write(LogLevel.ERROR, "Merger is external linked compartment");
            else
                if (Kidney > merger) Kidney--; 
            #endregion

            ModelHelper.SetArrayLength<double?>(ref _compartmentPositionX, _compartmentCount, merger);
            ModelHelper.SetArrayLength<double?>(ref _compartmentPositionY, _compartmentCount, merger);
        }

        public void FillCombo(ComboBox combo)
        {
            combo.Items.Clear();
            for (int i = 0; i < this.CompartmentCount; i++)
                combo.Items.Add(CompartmentNames[i]);
        }
    }

    [Serializable]
    public struct CompartmentArray
    {
        [XmlAttribute]
        public string Name;

        public string[] Compartments;

        public static CompartmentArray[] GetFromIntArray(int[][] array, string[] cids, string[] linkedids)
        {
            CompartmentArray[] res = new CompartmentArray[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    res[i] = new CompartmentArray() { Name = cids[i] };
                    res[i].Compartments = new string[array[i].Length];
                    for (int j = 0; j < array[i].Length; j++)
                        res[i].Compartments[j] = linkedids[array[i][j]];
                }
                return res;
        }

        private static int indexof(string[] array, string record)
        {
            for (int i = 0; i < array.Length; i++)
			{
                if (array[i] == record)
                    return i;
			}
            return -1;
        }
        public static int[][] ConvertToIntArray(CompartmentArray[] data, int compartmentCount, string[] cids, string[] linkedids)
        {
            try
            {
                int[][] res = new int[compartmentCount][];
                for (int i = 0; i < data.Length; i++)
                {
                    CompartmentArray li = data[i];
                    int cnum = indexof(cids, li.Name);
                    if (res[cnum] != null)
                        Log.Write(LogLevel.WARN, "Links from [{0}] defined twice. First definition ignored", cids[cnum]);
                    res[cnum] = new int[li.Compartments.Length];
                    for (int j = 0; j < li.Compartments.Length; j++)
                        res[cnum][j] = indexof(linkedids, li.Compartments[j]);
                }
                return res;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                throw ex;
            }
        }
    }

}
