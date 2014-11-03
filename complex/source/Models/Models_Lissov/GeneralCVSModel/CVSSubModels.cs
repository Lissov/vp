using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LissovLog;
using Model_Baroreception;
using Model_CVS;
using Model_Kidney;
using Model_Load;
using Model_Fluids;
using Model_HeartStable;
using Model_Energy;

namespace GeneralCVSModel
{
    internal static class CVSSubModels
    {
        internal static BaroreceptionModel BaroreceptionModel = null;
        internal static CVSModel VsModel = null;
        internal static KidneyModel KidneyModel = null;
        internal static LoadModel LoadModel = null;
        internal static FluidsModel FluidsModel = null;
        internal static HeartStableModel HeartModel = null;
        internal static EnergyModel EnergyModel = null;

        private static T GetModel<T>(GeneralModel model)
            where T : class
        {
            foreach (var item in model.Configuration.Models)
            {
                if (item is T)
                    return item as T;
            }
            return null;
        }

        private static bool _inited = false;
        public static bool Inited { get { return _inited; } }

        public static void InitModels(GeneralModel model)
        {
            BaroreceptionModel = GetModel<BaroreceptionModel>(model);
            VsModel = GetModel<CVSModel>(model);
            KidneyModel = GetModel<KidneyModel>(model);
            LoadModel = GetModel<LoadModel>(model);
            FluidsModel = GetModel<FluidsModel>(model);
            HeartModel = GetModel<HeartStableModel>(model);
            EnergyModel = GetModel<EnergyModel>(model);

            Log.Assert(BaroreceptionModel != null, LogLevel.ERROR, "Baroreception model not found");
            Log.Assert(VsModel != null, LogLevel.ERROR, "Vascular System model not found");
            Log.Assert(KidneyModel != null, LogLevel.ERROR, "Kidney model not found");
            Log.Assert(LoadModel != null, LogLevel.ERROR, "Loads model not found");
            Log.Assert(FluidsModel != null, LogLevel.ERROR, "Fluids model not found");
            Log.Assert(HeartModel != null, LogLevel.ERROR, "Heart model not found");
            Log.Assert(EnergyModel != null, LogLevel.ERROR, "Energy model not found");

            _inited = true;
        }

        public static void TryInit(GeneralModel model)
        {
            if (!_inited)
                InitModels(model);
        }
    }
}
