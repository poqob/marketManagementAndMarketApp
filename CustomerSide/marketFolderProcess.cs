using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;

namespace PQContentWidget
{
    static public class MarketFolderProcess
    {

        static string originalFileContent;

        static string currentFilePath;
        static string currentFileContent;

        static string tempFilePath;
        static string tempFileContent;


        //creates new .tmp and .temp files.
        static public void stockNumArrangerAndFileOperations(ref string filePath, ref string photoPath, ref string explanation, ref string stock, ref string price, ref string brandAndName, int howManyProductAddedToChart, ref string explanationPath)
        {

            //adjusting-seperating- brand and name
            string[] brandName = new string[2] { brandAndName.Substring(0, brandAndName.IndexOf(" ")).Trim(), brandAndName.Substring(brandAndName.IndexOf(" ")).Trim() };
            //MessageBox.Show(brandName[0] + " " + brandName[1]); 0 is for brand, 1 is for product name like sweater.

            //creating .tmp file to work on it.
            if (File.Exists(filePath) && !File.Exists(filePath.Substring(0, filePath.Length - 3) + "tmp"))
            {
                //copy file with .tmp extension -working on it- 
                File.Copy(filePath, filePath.Substring(0, filePath.Length - 3) + "tmp");
                currentFilePath = filePath.Substring(0, filePath.Length - 3) + "tmp";

                //copy .tmp file to store remain stock number for market product.
                File.Copy(filePath, filePath.Substring(0, filePath.Length - 3) + "temp");
                tempFilePath = filePath.Substring(0, filePath.Length - 3) + "temp";
                //rewrite the .temp content according to minusing stock number, that file contains remain stock number from market file.
                tempFileContent = "&" + brandName[1] + ":" + (Convert.ToInt32(stock) - howManyProductAddedToChart).ToString() + "," + (Convert.ToInt32(price) * (Convert.ToInt32(stock) - howManyProductAddedToChart)).ToString() + "$\n";
                tempFileContent += "&" + photoPath + "\n";
                tempFileContent += "&" + explanationPath + "\n";
                File.WriteAllText(tempFilePath, tempFileContent);

                //rewrite file content while customer increase or decrease order number. this file contains current order number according to customer input.
                currentFileContent = "&" + brandName[1] + ":" + howManyProductAddedToChart.ToString() + "," + (Convert.ToInt32(price) * howManyProductAddedToChart).ToString() + "$\n";
                currentFileContent += "&" + photoPath + "\n";
                currentFileContent += "&" + explanationPath + "\n";

                //rewrite file content.
                File.WriteAllText(currentFilePath, currentFileContent);
                //originalFileContent = File.ReadAllText(filePath);
            }


            //if file was already exist work on it's stock number.
            else if (File.Exists(filePath) && File.Exists(filePath.Substring(0, filePath.Length - 3) + "tmp"))
            {
                //to ensure.
                currentFilePath = filePath.Substring(0, filePath.Length - 3) + "tmp";
                tempFilePath = filePath.Substring(0, filePath.Length - 3) + "temp";

                //rewrite the .temp content according to minusing stock number, that file contains remain stock number from market file.
                tempFileContent = "&" + brandName[1] + ":" + (Convert.ToInt32(stock) - howManyProductAddedToChart).ToString() + "," + (Convert.ToInt32(price) * (Convert.ToInt32(stock) - howManyProductAddedToChart)).ToString() + "$\n";
                tempFileContent += "&" + photoPath + "\n";
                tempFileContent += "&" + explanationPath + "\n";
                File.WriteAllText(tempFilePath, tempFileContent);

                //rewrite file content.
                currentFileContent = "&" + brandName[1] + ":" + howManyProductAddedToChart.ToString() + "," + (Convert.ToInt32(price) * howManyProductAddedToChart).ToString() + "$\n";
                currentFileContent += "&" + photoPath + "\n";
                currentFileContent += "&" + explanationPath + "\n";
                File.WriteAllText(currentFilePath, currentFileContent);

                //if related product's tmp file's order number equal to zero ,it means there is no any order, delete order file.
                if (howManyProductAddedToChart == 0 && File.Exists(currentFilePath) && File.Exists(tempFilePath))
                {
                    File.Delete(currentFilePath);
                    File.Delete(tempFilePath);
                }
            }

        }

        static public void orderControll()
        {
            //find all .tmp files and make an array with them. in orders folder
            System.Collections.Generic.IEnumerable<string> files = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.tmp", SearchOption.AllDirectories);

            foreach (string file in files)
            {

                






            }

        }

        static public void destructor()
        {
            //find all .tmp files and make an array with them. in orders folder
            System.Collections.Generic.IEnumerable<string> filesTmp = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.tmp", SearchOption.AllDirectories);
            System.Collections.Generic.IEnumerable<string> filesTemp = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.temp", SearchOption.AllDirectories);

            foreach (string file in filesTmp)
            {
                File.Delete(file);
            }
            foreach (string file in filesTemp)
            {
                File.Delete(file);
            }

        }
    }




    //take file path, 
    //crate copy of that file with .tmp extension just in case order instruction keep stock num without ordered products.
    //create copy of that file with .temp extension and store how many product is ordering.
    //work on .tmp file

    //process: numericUpDown
    //arranging stock num for .temp and .tmp
    //if order have given, delete current .txt file and make .tmp->.txt;
    //send .temp file to customer$order file make extension .txt;
    //controlls if there was same file in customer$order:
    //if there was, merge two files and add one of them stock number to another one. same for totalPrice.
    //if there wasn't, do only moving. 


    /*
        the algorithm,
    read original file stock and keep it,
    create a copy of order file but it's stock and price is dynamicly changing.
    if order have given:
     edit .txt files stock and total price-minusing with howManyProductAddedToChart- .Replace()
     move copy file to customer's $order file.
    */

}