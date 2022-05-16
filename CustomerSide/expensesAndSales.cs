using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;

namespace ManagerSide
{
    static public class ExpensesAndSales
    {
        //income files start with I.
        //spends files Start with S.
        //usualSpends start with U.

        static string todayFilePath;
        static public void folderControl()
        {
            string today = DateTime.Today.Day.ToString();
            today += "-" + DateTime.Today.Month.ToString();
            today += "-" + DateTime.Today.Year.ToString().Trim().ToLower();
            string path = @"ManagerSide\datas\giro\" + today;
            todayFilePath = path + "\\dailySpends.txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                StreamWriter sw = new StreamWriter(path + "\\dailySpends.txt");
                sw.WriteLine("bills:500$");
                sw.WriteLine("employeeWage:1500$");
                sw.WriteLine("taxes:650$");
                sw.WriteLine("sells:0$");
                sw.WriteLine("orders:0$");
                sw.WriteLine("giro:-2650$");
                sw.Close();
                sw.Dispose();
            }
        }

        //local static variables.
        static int index0;
        static int index1;
        static string tmp;
        static string tmp1;

        //arrange wages and incomes from customer functions.
        //we call these functions everytime we give order or take order.

        static public void spends(string spend)
        {
            if (File.Exists(todayFilePath))
            {
                tmp = File.ReadAllText(todayFilePath);

                index0 = tmp.IndexOf("sells:") + 6;
                index1 = tmp.IndexOf("$", index0);
                //fetch related money data.
                tmp1 = tmp.Substring(index0, index1 - index0);

                //writing new money data to related row.
                tmp = tmp.Remove(index0, index1 - index0);
                tmp = tmp.Insert(index0, (Convert.ToInt32(tmp1) + Convert.ToInt32(spend)).ToString());

                File.WriteAllText(todayFilePath, tmp);
            }

        }
        static public void income(string income)
        {
            if (File.Exists(todayFilePath))
            {
                tmp = File.ReadAllText(todayFilePath);

                index0 = tmp.IndexOf("orders:") + 7;
                index1 = tmp.IndexOf("$", index0);
                //fetch related money data.
                tmp1 = tmp.Substring(index0, index1 - index0);

                //reorder old money data with new one.  (new+old)

                //writing new money data to related row.
                tmp = tmp.Remove(index0, index1 - index0);
                tmp = tmp.Insert(index0, (Convert.ToInt32(tmp1) + Convert.ToInt32(income)).ToString());

                File.WriteAllText(todayFilePath, tmp);
            }
        }

        static public void giro()
        {

            int bills;
            int employeeWage;
            int taxes;
            int sells;
            int orders;
            int sum;



            if (File.Exists(todayFilePath))
            {
                tmp = File.ReadAllText(todayFilePath);

                //fetching bills data
                index0 = tmp.IndexOf("bills:") + 6;
                index1 = tmp.IndexOf("$", index0);

                bills = Convert.ToInt32(tmp.Substring(index0, index1 - index0));

                //fetching employee wage data
                index0 = tmp.IndexOf("employeeWage:") + 13;
                index1 = tmp.IndexOf("$", index0);

                employeeWage = Convert.ToInt32(tmp.Substring(index0, index1 - index0));


                //fetching taxes data
                index0 = tmp.IndexOf("taxes:") + 6;
                index1 = tmp.IndexOf("$", index0);

                taxes = Convert.ToInt32(tmp.Substring(index0, index1 - index0));



                //fetching sells data
                index0 = tmp.IndexOf("sells:") + 6;
                index1 = tmp.IndexOf("$", index0);

                sells = Convert.ToInt32(tmp.Substring(index0, index1 - index0));


                //fetching orders data
                index0 = tmp.IndexOf("orders:") + 7;
                index1 = tmp.IndexOf("$", index0);

                orders = Convert.ToInt32(tmp.Substring(index0, index1 - index0));

                //fetching giro data
                index0 = tmp.IndexOf("giro:") + 5;
                index1 = tmp.IndexOf("$", index0);



                sum = orders + sells + employeeWage + bills + taxes;

                //writing new giro data to giro row.
                tmp = tmp.Remove(index0, index1 - index0);
                tmp = tmp.Insert(index0, sum.ToString());
                File.WriteAllText(todayFilePath, tmp);
            }
        }
    }


}