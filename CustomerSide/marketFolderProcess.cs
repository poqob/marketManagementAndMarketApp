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

        static public void orderControll(ref string folderAdress, ref string customerName)
        {
            //find all .tmp files and make an array with them that is in productsForSale folder, in order to move .tmp to customer$order folder.
            System.Collections.Generic.IEnumerable<string> filesTmp = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.tmp", SearchOption.AllDirectories);
            //find all .temp files and make an array with them that is in productsForSale folder,  in order to make .temps new .txts
            System.Collections.Generic.IEnumerable<string> filesTemp = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.temp", SearchOption.AllDirectories);

            //customer$order
            string customerOrderFolder = folderAdress + "\\" + customerName + "$order";

            //files gooese
            foreach (string file in filesTmp)
            {
                //send .tmp file to customer$order
                if (!File.Exists(customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt"))
                {
                    File.Move(file, customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt");
                }
                else
                {
                    //to get stock nums.
                    string oldStock;
                    string newStock;

                    //to get prices.
                    string newPrice;
                    string oldPrice;

                    //temporary string data that keeps file content.
                    string temporaryContentFile;

                    //readşng indexes.
                    int index0;
                    int index1;

                    //firstly i attempt new file because we read new file which is .tmp we will add it to .txt
                    temporaryContentFile = File.ReadAllText(file);

                    //getting stock number from new file.
                    index0 = temporaryContentFile.IndexOf(":") + 1;
                    index1 = temporaryContentFile.IndexOf(",");
                    //attemting new stock number to newStock.
                    newStock = temporaryContentFile.Substring(index0, index1 - index0);

                    //attempting newTotalPrice to newPrice
                    index0 = temporaryContentFile.IndexOf("$");
                    newPrice = temporaryContentFile.Substring(index1 + 1, index0 - index1 - 1);


                    //secondly i attempt old file because we will add new one's products and stock to old. because old(.txt) have already located in customer$order folder.
                    temporaryContentFile = File.ReadAllText(customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt");

                    //getting stock number from new file.
                    index0 = temporaryContentFile.IndexOf(":") + 1;
                    index1 = temporaryContentFile.IndexOf(",");
                    //attemting old stock number to newStock.
                    oldStock = temporaryContentFile.Substring(index0, index1 - index0);

                    //attempting oldTotalPrice to newPrice
                    index0 = temporaryContentFile.IndexOf("$");
                    oldPrice = temporaryContentFile.Substring(index1 + 1, index0 - index1 - 1);

                    //changing stock number.
                    temporaryContentFile = temporaryContentFile.Replace(oldStock, (Convert.ToInt32(oldStock) + Convert.ToInt32(newStock).ToString()));

                    //changing total price.
                    temporaryContentFile = temporaryContentFile.Replace(oldPrice, (Convert.ToInt32(oldPrice) + Convert.ToInt32(newPrice).ToString()));

                    //applying changes to the .txt file.
                    File.WriteAllText(customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt", temporaryContentFile);







                }
            }
            //deleting .tmp and .temp files 
            //not: issume i didn't use any .temp file.
            foreach (string file in filesTmp)
            {
                File.Delete(file);
            }
            foreach (string file in filesTemp)
            {
                File.Delete(file);
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




    /*
        the algorithm,
    read original file stock and keep it,
    create a copy of order file but it's stock and price is dynamicly changing.
    if order have given:
     edit .txt files stock and total price-minusing with howManyProductAddedToChart- .Replace()
     move copy file to customer's $order file.
    */

}