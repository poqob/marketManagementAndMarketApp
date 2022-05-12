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
    public class Market : Form
    {   //variables.
        RJButton menuButton = new RJButton();
        FlowLayoutPanel panel = new FlowLayoutPanel();
        RoundLabel lable0 = new RoundLabel();//ordered products label, i'm thinking make flow panel or panle there
        RoundLabel label1 = new RoundLabel();//market, balance and buttons label

        string marketPath = @"ManagerSide\datas\productsForSale";

        //folder adress is 
        string customerName;
        //folder adress is keeps customer $order file.
        string folderAdress;
        public Market(ref string customerName, ref string folderAdress)
        {
            this.customerName = customerName;
            this.folderAdress = folderAdress;
            baseCreator();
            productLoader();
        }

        private void baseCreator()
        {
            //local variables
            Point point = new Point(20, 85);
            BackColor = Color.FromArgb(255, 230, 204);
            Font ItalicFont = new Font("Arial", 12, FontStyle.Italic);


            //marketBorder
            lable0.Size = new Size(610, 340);
            lable0.Location = new Point(180, 85);
            lable0.backColor = BackColor;
            lable0.borderColor = Color.Black;
            lable0.Font = ItalicFont;
            lable0.borderWidth = 2;
            lable0.cornerRadius = 60;
            lable0.ForeColor = Color.Black;
            lable0.Tag = "base";
            Controls.Add(lable0);

            //order section
            label1.Size = new Size(140, 340);
            label1.Location = point;
            label1.backColor = BackColor;
            label1.borderColor = Color.Black;
            label1.borderWidth = 2;
            label1.cornerRadius = 60;
            label1.ForeColor = Color.Black;
            label1.Tag = "base";
            Controls.Add(label1);


            //order button
            menuButton.Size = new Size(100, 60);
            menuButton.Location = new Point(40, 350);
            menuButton.BackColor = Color.FromArgb(255, 230, 204);
            menuButton.BorderColor = Color.Black;
            menuButton.BorderRadius = 15;
            menuButton.BorderSize = 1;
            menuButton.Text = "order";
            menuButton.Font = ItalicFont;
            menuButton.ForeColor = Color.Black;
            menuButton.Click += delegate (object sender, EventArgs e) { };
            Controls.Add(menuButton);
            menuButton.BringToFront();


            //flow panel that keeps our widgets.
            panel.Location = new Point(195, 100);
            panel.Size = new Size(580, 300);
            panel.BackColor = BackColor;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.AutoScroll = true;
            panel.WrapContents = true;
            Controls.Add(panel);
            panel.BringToFront();

            //app bar
            MarketBar marketBar = new MarketBar(customerName, getBalance(folderAdress), ref folderAdress);
            Controls.Add(marketBar);


        }

        //to get balance value from customer information file.
        private string getBalance(string folderAdress)
        {

            //folder adress.
            folderAdress += "\\" + customerName + "$info\\data.txt";

            string balance = File.ReadAllText(folderAdress);

            //money range.
            int index = balance.IndexOf("balance:") + 8;

            //substringing.
            balance = balance.Substring(index, balance.Length - index - 2);

            return balance;
        }

        //TODO: shopping system
        //list all products
        //add them to shopping list with shoppingList staticly widgets by increasing or decreasing products.
        //when user have pressed order button, clear shop list and make necessary moves.
        //-from market folder to customer order folder, create one copy of file to manager side order folder-


        private void productLoader()
        {

            string fileContent;
            int index0;
            int index1;
            string stockNum;
            string unitPrice;
            string photoPath;
            string explanationPath;
            string explanation;
            string brandAndName = "";
            string[] catagories = { "WOMAN", "MAN", "CHILD" };
            string[] catagoriesOfWear = { "t_shirt", "sweater", "pants" };

            System.Collections.Generic.IEnumerable<string> files = Directory.EnumerateFiles(marketPath, "*.txt", SearchOption.TopDirectoryOnly);





            //panel clears itself because if isn't new generated widgets will have piled up.
            panel.Controls.Clear();
            foreach (string file in files)
            {

                //arranging brandAndName variable.
                foreach (string catagory in catagories)
                {
                    if (file.Contains(catagory))
                    {
                        //to pretend MAN-WOMAN confuse, man word in woman word that cause a confuse.
                        if (catagory == "MAN" && file.Contains("WOMAN"))
                        {
                            continue;
                        }
                        else
                        {
                            index0 = file.IndexOf(catagory);
                            //naming brandAndName, firstly brand
                            brandAndName = file.Substring(folderAdress.Length + 2, index0 - folderAdress.Length - 2);
                            foreach (string wear in catagoriesOfWear)
                            {
                                if (file.Contains(wear))
                                {
                                    brandAndName += " " + wear;
                                }
                            }
                        }
                    }
                }

                fileContent = File.ReadAllText(file);

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

                MarketContentWidget marketContentWidget = new MarketContentWidget(ref photoPath, unitPrice, ref stockNum, ref brandAndName, ref explanation);
                panel.Controls.Add(marketContentWidget);
            }
        }
    }
}

//LOOK TODO FIle