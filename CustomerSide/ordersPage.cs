using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CustomControls.RJControls;
using RoundBorderLabel;
using System.Text.RegularExpressions;
using PQContentWidget;

namespace OrdersPage
{



    public class OrdersPage : Form
    {
        //variables for appbar.
        string customerName;
        string balance;
        string folderAdress;

        //form members.
        RoundLabel label2 = new RoundLabel();

        Label labelName = new Label();

        Label balanceLabel = new Label();

        RoundLabel label1 = new RoundLabel();
        RoundLabel lable0 = new RoundLabel();

        FlowLayoutPanel panel = new FlowLayoutPanel();


        public OrdersPage(ref string customerName, ref string balance, ref string folderAdres)
        {
            this.customerName = customerName;
            this.balance = balance;
            this.folderAdress = folderAdres;

            this.Size = new Size(800, 500);
            this.CenterToParent();
            this.BackColor = Color.FromArgb(255, 230, 204);
            baseCreator();

        }
        private void baseCreator()
        {

            this.Size = new Size(800, 500);
            this.Location = new Point(15, 5);
            this.BackColor = Color.FromArgb(255, 230, 204);


            //balanceLabel label
            label2.Location = new Point(165, 10);
            label2.Size = new Size(610, 55);
            label2.BackColor = Color.FromArgb(255, 230, 204);
            label2.borderColor = Color.Black;
            label2.cornerRadius = 20;
            label2.borderWidth = 2;
            label2.BringToFront();
            Controls.Add(label2);


            //balance label texts
            //name label
            labelName.Location = new Point(10, 8);
            labelName.Size = new Size(110, 40);
            labelName.BackColor = Color.FromArgb(255, 230, 204);
            labelName.Font = new Font("Arial", 11, FontStyle.Regular);
            labelName.Text = customerName;
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            labelName.BringToFront();
            label2.Controls.Add(labelName);

            //balance label
            balanceLabel.Location = new Point(125, 8);
            balanceLabel.Size = new Size(150, 40);
            balanceLabel.BackColor = Color.FromArgb(255, 230, 204);
            balanceLabel.Font = new Font("Arial", 8, FontStyle.Regular);
            balanceLabel.Text = "balance: " + balance + "$";
            balanceLabel.TextAlign = ContentAlignment.MiddleCenter;
            balanceLabel.BringToFront();
            label2.Controls.Add(balanceLabel);

            //order label
            label1.Location = new Point(5, 10);
            label1.Size = new Size(140, 55);
            label1.BackColor = Color.FromArgb(255, 230, 204);
            label1.borderColor = Color.Black;
            label1.borderWidth = 2;
            label1.cornerRadius = 20;
            label1.Text = "Orders";
            label1.Font = new Font("Arial", 12, FontStyle.Regular);
            label1.BringToFront();
            Controls.Add(label1);

            //marketBorder
            lable0.Size = new Size(770, 360);
            lable0.Location = new Point(5, 85);
            lable0.backColor = BackColor;
            lable0.borderColor = Color.Black;
            lable0.borderWidth = 2;
            lable0.cornerRadius = 60;
            lable0.ForeColor = Color.Black;
            lable0.Tag = "base";
            Controls.Add(lable0);

            panel.Location = new Point(20, 20);
            panel.Size = new Size(730, 320);
            panel.BackColor = BackColor;
            panel.WrapContents = true;
            panel.FlowDirection = FlowDirection.LeftToRight;
            lable0.Controls.Add(panel);

            productLoader();
        }

        // load ordered products into panel from customer's order file.
        private void productLoader()
        {
            //widgets will created here.
            folderAdress = folderAdress + "\\" + customerName + "$order";

            System.Collections.Generic.IEnumerable<string> files = Directory.EnumerateFiles(folderAdress, "*.txt", SearchOption.TopDirectoryOnly);
            //panel clears itself because if isn't new generated widgets will have piled up.
            panel.Controls.Clear();

            foreach (string file in files)
            {

                orderFileReaderAndWidgetCreator(file, folderAdress.Length);
            }
        }
        //folder adress lenght is neccesary because of substringing operation  in foreach loop.
        private void orderFileReaderAndWidgetCreator(string fileAdress, int folderAdresLenghtWithoutCustomerName)
        {

            string[] catagories = { "WOMAN", "MAN", "CHILD" };
            string[] catagoriesOfWear = { "t_shirt", "sweater", "pants" };

            string fileContent;

            string unitPrice;
            string stockNum;
            string photoPath;
            string explanationPath;
            string explanation;
            string brandAndName = "";

            int index0;
            int index1;

            //take all data in.
            fileContent = File.ReadAllText(fileAdress);

            //arranging brandAndName variable.
            foreach (string catagory in catagories)
            {
                if (fileAdress.Contains(catagory))
                {
                    //to pretend MAN-WOMAN confuse, man word in woman word that cause a confuse.
                    if (catagory == "MAN" && fileAdress.Contains("WOMAN"))
                    {
                        continue;
                    }
                    else
                    {
                        index0 = fileAdress.IndexOf(catagory);
                        //naming brandAndName, firstly brand
                        brandAndName = fileAdress.Substring(folderAdresLenghtWithoutCustomerName + 1, index0 - folderAdresLenghtWithoutCustomerName - 1);
                        foreach (string wear in catagoriesOfWear)
                        {
                            if (fileAdress.Contains(wear))
                            {
                                brandAndName += " " + wear;
                            }
                        }
                    }
                }
            }

            //to get stock number from fileContent.
            index0 = fileContent.IndexOf(":") + 1;
            index1 = fileContent.IndexOf(",");
            stockNum = fileContent.Substring(index0, index1 - index0);

            //to get total price and convert it to a unit price.
            index0 = fileContent.IndexOf("$", index1);
            unitPrice = fileContent.Substring(index1 + 1, index0 - index1 - 1);//it is total price
            //to obtain unit price via dividing totalPrice by stockNumber.
            unitPrice = Convert.ToInt32(Convert.ToInt32(unitPrice) / Convert.ToInt32(stockNum)).ToString();

            //to get photo number.
            index0 = fileContent.IndexOf("&", 2);
            index1 = fileContent.IndexOf("\n", index0);
            photoPath = fileContent.Substring(index0 + 1, index1 - index0 - 1);
            photoPath = photoPath.Insert(0, "ManagerSide\\");

            //to get explanation path
            index0 = fileContent.IndexOf("&", index1) + 1;
            index1 = fileContent.IndexOf("\n", index0);
            explanationPath = fileContent.Substring(index0, index1 - index0);
            explanationPath = explanationPath.Insert(0, "ManagerSide\\");

            //to get explanation.
            explanation = File.ReadAllText(explanationPath);


            StocksWidget marketContentWidget = new StocksWidget(ref photoPath, unitPrice, ref stockNum, ref brandAndName, ref explanation);
            panel.Controls.Add(marketContentWidget);

        }
    }
}