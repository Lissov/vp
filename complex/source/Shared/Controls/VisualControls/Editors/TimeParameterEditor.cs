using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LissovBase;

namespace VisualControls.Editors
{
    public partial class TimeParameterEditor : BaseParameterEditor
    {
        public TimeParameterEditor()
        {
            InitializeComponent();

            Unit = string.Empty;

            CreateEditors();
        }

        public override string Unit
        {
            get
            {
                return base.Unit;
            }
            set
            {
                base.Unit = string.Empty;
            }
        }

        private const int timeCount = 4;
        private Panel[] timePanels = new Panel[timeCount];
        private TextBox[] timeEditors = new TextBox[timeCount];
        private double[] timeMultipliers = new double[timeCount] {
                1, 60, 3600, 3600 * 24
        };
        private Label[] timeLabelControls = new Label[timeCount];
        private void CreateEditors()
        {
            string[] timeLabels = new string[timeCount] {
                Constants.Units.second,
                Constants.Units.minute,
                Constants.Units.hour,
                Constants.Units.day
            };


            for (int i = timeCount - 1; i >= 0; i--)
            {
                Panel p = new Panel();
                p.Left = 100;
                p.Width = 70;
                panEditors.Controls.Add(p);
                p.Dock = DockStyle.Left;

                TextBox tb = new TextBox();
                p.Controls.Add(tb);
                tb.Left = 0;
                tb.Width = p.Width - 80;
                tb.Top = 0;
                tb.Height = p.Height;
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                tb.Tag = i;
                tb.TabIndex = i;
                tb.TextAlign = HorizontalAlignment.Right;
                tb.TextChanged += new EventHandler(tb_TextChanged);
                timeEditors[i] = tb;

                Label l = new Label();
                l.Text = timeLabels[i];
                l.Top = 2;
                l.Left = tb.Right + 8;
                p.Controls.Add(l);
                l.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                timeLabelControls[i] = l;

                timePanels[i] = p;
            }
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int i = (int)(sender as TextBox).Tag;
            if (timesetting) return;
            double r;
            string text = (sender as TextBox).Text;
            if (double.TryParse(text, out r))
            {
                double m = timeMultipliers[i];
                SetTimeValue(r * m, i);
                OnValueChanged();
            }
            else
                errorProvider1.SetError(timeLabelControls[i], "Please enter correct double value");
        }

        private void panEditors_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < timeCount; i++)
            {
                timePanels[i].Width = panEditors.Width / timeCount;
            }
        }

        private bool timesetting = false;

        public double TimeValue
        {
            get
            {
                double res;
                if (double.TryParse(timeEditors[0].Text, out res))
                    return res;
                return 0;
            }
            set
            {
                SetTimeValue(value, -1);
            }
        }

        private void SetTimeValue(double value, int exceptEditor)
        {
            timesetting = true;
            for (int i = 0; i < timeCount; i++)
            {
                if (i == exceptEditor)
                    continue;
                double m = timeMultipliers[(int)timeEditors[i].Tag];
                timeEditors[i].Text = Math.Round(value / m, 6).ToString();
            }
            timesetting = false;
        }

        public override object Value
        {
            get
            {
                return TimeValue;
            }
            set
            {
                TimeValue = (double)value;
            }
        }
    }
}
