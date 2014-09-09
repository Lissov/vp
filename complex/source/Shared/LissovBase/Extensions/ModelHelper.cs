using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LissovBase
{
    public static class ModelHelper
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

        public static void SetArrayLength<T>(ref T[] array, int newLength, int toremove)
        {
            SetArrayLength<T>(ref array, newLength, new List<int>(new int[] { toremove }));
        }

        public static void SetArrayLength<T>(ref T[] array, int newLength, List<int> toremove)
        {
            if (toremove == null || array == null || array.Length == 0)
            {
                array = new T[newLength];
                return;
            }

            T[] res = new T[newLength];
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (!toremove.Contains(i))
                {
                    res[index] = array[i];
                    index++;
                }
            }
            array = res;
        }

        public static T[] AddToArray<T>(ref T[] arr, T value)
        {
            T[] res = new T[arr.Length + 1];
            arr.CopyTo(res, 0);
            res[res.Length - 1] = value;
            return res;
        }
    }
}
