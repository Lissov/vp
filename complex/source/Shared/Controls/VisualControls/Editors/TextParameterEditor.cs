using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualControls.Editors
{
    public class TextParameterEditor : BaseParameterEditor
    {
        private System.Windows.Forms.TextBox textBox1;
    
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panEditors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panEditors.Controls.Add(this.textBox1);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // DoubleParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "DoubleParameterEditor";
            this.panEditors.ResumeLayout(false);
            this.panEditors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public TextParameterEditor()
            :base()
        {
            InitializeComponent();
        }

        public string TextValue
        {
            get { return textBox1.Text; }
            set
            {
                if (textBox1 == null)
                    return;
                textBox1.Text = value;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            OnValueChanged();
        }

        public override object Value
        {
            get { return TextValue; }
            set { TextValue = (string)value; }
        }
    }
}
