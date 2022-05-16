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

        static string remainBalance;

        //creates new .tmp and .temp files.
        static public void stockNumArrangerAndFileOperations(ref string filePath, string photoPath, ref string explanation, ref string stock, ref string price, ref string brandAndName, int howManyProductAddedToChart, string explanationPath)
        {

            //adjusting-seperating- brand and name
            string[] brandName = new string[2] { brandAndName.Substring(0, brandAndName.IndexOf(" ")).Trim(), brandAndName.Substring(brandAndName.IndexOf(" ")).Trim() };
            //MessageBox.Show(brandName[0] + " " + brandName[1]); 0 is for brand, 1 is for product name like sweater.

            int index0;

            //adjusting directories.
            //deleting managerSide relative paths from photopath and explanation path line.
            index0 = photoPath.IndexOf("Manager");
            photoPath = photoPath.Remove(index0, 12);
            index0 = explanationPath.IndexOf("Manager");
            explanationPath = explanationPath.Remove(index0, 12);

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
            else if (File.Exists(filePath) && File.Exists(filePath.Substring(0, filePath.Length - 3) + "temp"))
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

        static public void orderControll(ref string folderAdress, ref string customerName, Control form)
        {
            //find all .tmp files and make an array with them that is in productsForSale folder, in order to move .tmp to customer$order folder.
            System.Collections.Generic.IEnumerable<string> filesTmp = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.tmp", SearchOption.AllDirectories);
            //find all .temp files and make an array with them that is in productsForSale folder,  in order to make .temps new .txts
            System.Collections.Generic.IEnumerable<string> filesTemp = Directory.EnumerateFiles(@"ManagerSide\datas\productsForSale\", "*.temp", SearchOption.AllDirectories);

            //customer$order
            string customerOrderFolder = folderAdress + "\\" + customerName + "$order";
            //customer info
            string customerInfoFolder = folderAdress + "\\" + customerName + "$info";

            //to get stock nums.
            string oldStock;
            string newStock;

            //to get prices.
            string newPrice;
            string oldPrice;

            //temporary string data that keeps file content.
            string temporaryContentFile;

            //indexes.
            int index0;
            int index1;


            foreach (string file in filesTmp)
            {
                //send .tmp file to customer$order
                if (!File.Exists(customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt"))
                {

                    //firstly i attempt new file because we read new file which is .tmp we will add it to .txt
                    temporaryContentFile = File.ReadAllText(file);
                    //attempting newTotalPrice to newPrice
                    index1 = temporaryContentFile.IndexOf(",");
                    index0 = temporaryContentFile.IndexOf("$");
                    newPrice = temporaryContentFile.Substring(index1 + 1, index0 - index1 - 1);
                    File.Move(file, customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt");
                    moneyManagement(ref newPrice, ref customerInfoFolder);
                }
                else
                {

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
                    moneyManagement(ref newPrice, ref customerInfoFolder);


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
                    temporaryContentFile = temporaryContentFile.Replace(oldStock, (Convert.ToInt32(oldStock) + Convert.ToInt32(newStock)).ToString());

                    //changing total price.
                    temporaryContentFile = temporaryContentFile.Replace(oldPrice, (Convert.ToInt32(oldPrice) + Convert.ToInt32(newPrice)).ToString());



                    //applying changes to the .txt file that is ordered product.
                    File.WriteAllText(customerOrderFolder + "\\" + file.Substring(34, file.Length - 37) + "txt", temporaryContentFile);

                }
            }

            foreach (string file in filesTemp)
            {

                //read .temp file.
                temporaryContentFile = File.ReadAllText(file);

                //getting stock number.
                index0 = temporaryContentFile.IndexOf(":") + 1;
                index1 = temporaryContentFile.IndexOf(",");
                //attemting new stock number to newStock.
                newStock = temporaryContentFile.Substring(index0, index1 - index0);

                //if .temp file's stock number is equal to 0, we dont need to make it .txt and store in market folder which is productsForSale
                if (newStock != "0")
                {
                    File.Delete(file.Substring(0, file.Length - 4) + "txt");
                    File.Copy(file, file.Substring(0, file.Length - 4) + "txt");
                }
                else
                {
                    File.Delete(file.Substring(0, file.Length - 4) + "txt");
                }
            }

            //deleting .tmp and .temp files 
            foreach (string file in filesTmp)
            {
                File.Delete(file);
            }
            foreach (string file in filesTemp)
            {
                File.Delete(file);
            }

            MessageBox.Show("Order has taken.\nRemain Balance is: " + remainBalance, "Order receipt!");

            //doesnt work
            Utilities.ResetAllControls(form);

        }



        /*
    money management,

    while shopping calculate remain balance dynamicly.
    -create a copy of customer$info data.txt as .tmp
    -fetch money from .txt file.
    -get productForSale\*.tmp's total price and sum with each other.
    -return string that is equal to balance-tmp.totalCost
    -if order has given, edit customer$info data.txt's balance by last balance.
    -delete customer$info .tmp file.
    */
        static public void moneyManagement(ref string price, ref string customerInfoFolder)
        {
            //to get prices.
            string oldPrice = "1000";
            //indexes.
            int index0;

            string originalFile = customerInfoFolder + "\\data.txt";
            string tempFile = customerInfoFolder + "\\data.tmp";


            if (!File.Exists(tempFile))
            {
                File.Copy(originalFile, tempFile);
            }

            string temporaryContentFile = File.ReadAllText(tempFile);

            //attempting newTotalPrice to newPrice

            if (temporaryContentFile.Contains("&balance:"))
            {
                index0 = temporaryContentFile.IndexOf("&balance:");
                oldPrice = temporaryContentFile.Substring(index0 + 9);
                remainBalance = (Convert.ToInt32(oldPrice) - Convert.ToInt32(price)).ToString();
                temporaryContentFile = temporaryContentFile.Replace(oldPrice, remainBalance);
                File.WriteAllText(tempFile, temporaryContentFile);
            }

            if (File.Exists(tempFile))
            {
                File.Delete(originalFile);
                File.Copy(tempFile, originalFile);
                File.Delete(tempFile);
            }
        }




        //destructor of customerMarket page.
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

    //reset NumericUpDown form elements
    public static class Utilities
    {
        public static void ResetAllControls(Control form)
        {
            foreach (Control control in form.Controls)
            {
                //reset numericUpDown buttons
                if (control is NumericUpDown)
                {
                    NumericUpDown upDown = (NumericUpDown)control;
                    upDown.Value = 0;
                }

                if (control is MarketContentWidget)
                {
                    MarketContentWidget widget = (MarketContentWidget)control;
                    widget.Refresh();
                }

                if (control is MarketShoppingList)
                {
                    MarketShoppingList widget = (MarketShoppingList)control;
                    widget.Refresh();
                }
            }
        }
    }




    /*
        the basic thing behind these,

    read original file stock and keep it,
    create a copy of order file but it's stock and price is dynamicly changing.
    if order have given:
     edit .txt files stock and total price-minusing with howManyProductAddedToChart- .Replace()
     move copy file to customer's $order file.
    */



}