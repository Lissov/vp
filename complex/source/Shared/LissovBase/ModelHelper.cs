using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LissovBase
{
    public class ModelHelper
    {
        public static void LoadVPOleParams(LissovModelBase model)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        model.LoadOldModelParameters(dlg.FileName);
                        MessageBox.Show("Parameters loaded", "Load complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static double BoolToDouble(bool b)
        {
            return b ? LissovModelBase.TRUE : LissovModelBase.FALSE;
        }
    }
}
