using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model_HeartStable;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlHeartModel : IModelControlHeartStableModel
    {
        public ControlHeartModel()
        {
            InitializeComponent();
        }
    }

    public class IModelControlHeartStableModel : IModelControl<HeartStableModel> { }
}
