using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModelBase;

namespace GeneralCVSModel.ResultsTable
{
    public partial class AddOutputForm : Form
    {
        public AddOutputForm()
        {
            InitializeComponent();
        }

        Configuration _configuration;
        private void setup(Configuration configuration)
        {
            _configuration = configuration;
            comboModel.Items.Clear();
            foreach (var item in configuration.Models)
            {
                comboModel.Items.Add(item.GetName().ToString());
            }
        }

        private const string ALL_GROUP = "<ALL>";
        private void comboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboGroup.Items.Clear();
            IModel m = getModel((string)comboModel.SelectedItem);
            if (m == null) 
                return;

            List<string> groups = new List<string>();
            groups.Add(ALL_GROUP);
            foreach (var value in m.GetValues())
            {
                if (!string.IsNullOrEmpty(value.GroupName)
                    && !groups.Contains(value.GroupName))
                    groups.Add(value.GroupName);
            }
            comboGroup.Items.AddRange(groups.ToArray());
        }
        private IModel getModel(string name)
        {
            foreach (var item in _configuration.Models)
            {
                if (item.GetName() == name)
                    return item;
            }
            return null;
        }

        private void comboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboOutput.Items.Clear();
            IModel m = getModel((string)comboModel.SelectedItem);
            if (m == null)
                return;

            List<string> outputs = new List<string>();
            string group = (string)comboGroup.SelectedItem;
            foreach (var item in m.GetValues())
            {
                if (item.GroupName == group || group == ALL_GROUP)
                    outputs.Add(item.Name);
            }
            comboOutput.Items.AddRange(outputs.ToArray());
        }

        public static OutputInfo Execute(Configuration configuration)
        {
            AddOutputForm form = new AddOutputForm();
            form.setup(configuration);

            if (form.ShowDialog() == DialogResult.OK)
            {
                OutputInfo res = new OutputInfo();
                res.Model = form.getModel((string)form.comboModel.SelectedItem);
                res.OutputName = (string)form.comboOutput.SelectedItem;
                return res;
            }
            else
                return null;
        }
        
    }
 
    public class OutputInfo
    {
        public IModel Model;
        public string OutputName;
    }
}
