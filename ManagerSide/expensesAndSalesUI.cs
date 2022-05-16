using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;

namespace ManagerSide
{
    static class ExpensesAndSalesUI
    {




        static public void messager()
        {
            System.Collections.Generic.IEnumerable<string> files = Directory.EnumerateFiles(@"datas\giro", "*.txt", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                string tmp = File.ReadAllText(item);
                MessageBox.Show(tmp, item.Substring(10));
            }
        }




    }

}