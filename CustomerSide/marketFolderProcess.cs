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
        static string currentFilePath;
        static string currentFileContent;
        static string originalFileContent;
        static string originalFileStock;
        static string currentFileNewStock;
        static string currentFileOldStock;
        static string currentFileOldPrice;
        static string currentFileNewPrice;
        static string unitPrice;
        static int index0;
        static int index1;






        //creates new .tmp and .temp files.
        static public void stockNumArranger(ref string filePath, int howManyProductAddedToChart)
        {
            //creating .tmp file to work on it.
            if (File.Exists(filePath) && !File.Exists(filePath.Substring(0, filePath.Length - 3) + "tmp"))
            {
                File.Copy(filePath, filePath.Substring(0, filePath.Length - 3) + "tmp");

                currentFilePath = filePath.Substring(0, filePath.Length - 3) + "tmp";
                originalFileContent = File.ReadAllText(filePath);
            }


            //if file was already exist work on it's stock number.
            else if (File.Exists(filePath) && File.Exists(filePath.Substring(0, filePath.Length - 3) + "tmp"))
            {
                //reattempting just in case static struct of class.
                currentFilePath = filePath.Substring(0, filePath.Length - 3) + "tmp";
                currentFileContent = File.ReadAllText(currentFilePath);

                //to get stock number from originalFileContent.
                index0 = originalFileContent.IndexOf(":") + 1;
                index1 = originalFileContent.IndexOf(",");
                originalFileStock = originalFileContent.Substring(index0, index1 - index0);//.txt's stock number.

                //to get stock number from currentFileContent.
                index0 = currentFileContent.IndexOf(":") + 1;
                index1 = currentFileContent.IndexOf(",");
                currentFileOldStock = currentFileContent.Substring(index0, index1 - index0);//.tmp's stock number.

                //to get total price and convert it to a unit price.
                index1 = originalFileContent.IndexOf(",");
                index0 = originalFileContent.IndexOf("$", index1);
                //it is total price
                unitPrice = originalFileContent.Substring(index1 + 1, index0 - index1 - 1);

                //to obtain unit price via dividing totalPrice by stockNumber.                                                                     
                unitPrice = Convert.ToInt32(Convert.ToInt32(unitPrice) / Convert.ToInt32(originalFileStock)).ToString();//unit price.

                //to get unit price from current file that is .tmp
                index1 = currentFileContent.IndexOf(",");
                index0 = currentFileContent.IndexOf("$", index1);
                currentFileOldPrice = currentFileContent.Substring(index1 + 1, index0 - index1 - 1);


                currentFileNewPrice = (Convert.ToInt32(unitPrice) * Convert.ToInt32(howManyProductAddedToChart)).ToString();
                currentFileNewStock = (Convert.ToInt32(originalFileStock) - howManyProductAddedToChart).ToString();
                //replacing file stock num with new stock num.
                currentFileContent = currentFileContent.Replace(currentFileOldStock, howManyProductAddedToChart.ToString());
                MessageBox.Show("currentFileNewStock " + currentFileNewStock + " currentFileOldStock " + currentFileOldStock + " total pr " + unitPrice);

                //replacing curretn file's total price.
                //(Convert.ToInt32(currentFileUnitPrice) * (Convert.ToInt32(currentFileStock) - howManyProductAddedToChart)).ToString(), (Convert.ToInt32(currentFileUnitPrice) * howManyProductAddedToChart).ToString()
                currentFileContent = currentFileContent.Replace(Convert.ToInt32(Convert.ToInt32(unitPrice) * Convert.ToInt32(currentFileOldStock)).ToString(), (Convert.ToInt32(unitPrice) * howManyProductAddedToChart).ToString());
                File.WriteAllText(currentFilePath, currentFileContent);
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

}