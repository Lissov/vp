using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;

namespace GeneralCVSModel.ConfigurationEditor
{
    public partial class ControlNoModelSelected : IModelControlLissovModelBase
    {
        public ControlNoModelSelected()
        {
            InitializeComponent();
        }
    }

    public class IModelControlLissovModelBase : IModelControl<LissovModelBase> { }
}
